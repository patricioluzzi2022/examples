using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace services.Classes.NetStat
{
    public class NetStatClass
    {

        public NetStatClass() { }

        private static List<NetStatEntry> GetNetStatByProcessId(int processId)
        {
            var result = new List<NetStatEntry>();

            var psi = new ProcessStartInfo
            {
                FileName = "netstat",
                Arguments = "-ano",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            string output = process.StandardOutput.ReadToEnd();

            var lines = output
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(4); // salta encabezado

            foreach (var line in lines)
            {
                var parts = Regex.Split(line.Trim(), @"\s+");

                if (parts.Length < 5)
                    continue;

                if (!int.TryParse(parts[^1], out int pid))
                    continue;

                if (pid != processId || pid == 0)
                    continue;

                result.Add(new NetStatEntry
                {
                    Protocol = parts[0],
                    LocalAddress = parts[1],
                    ForeignAddress = parts[2],
                    State = parts.Length == 5 ? parts[3] : null,
                    ProcessId = pid
                });
            }

            return result;
        }

        public static async Task<List<NetStatEntry>> GetNetStatusByService(WindowServiceInfo service)
        {
            List<NetStatEntry> results = new List<NetStatEntry>();

            results = GetNetStatByProcessId(service.ProcessId);

            results.ForEach(async entry =>
            {
                try
                {
                    // ----------------------------
                    // CPU + Memoria (Windows)
                    // ----------------------------
                    var process = Process.GetProcessById(service.ProcessId);

                    entry.Memory = process.WorkingSet64 / 1024 / 1024;

                    entry.CPU = await GetCpuUsageAsync(entry.ProcessId);

                    // ----------------------------
                    // Signature
                    // ----------------------------
                    try
                    {
                        // 1. Obtener la ruta del proceso
                        string path = Process.GetProcessById(entry.ProcessId).MainModule.FileName;

                        // 2. Intentar cargar el certificado desde el archivo
                        // Nota: En .NET 9 (2025) se recomienda X509Certificate2 para más detalles
                        X509Certificate cert = X509Certificate.CreateFromSignedFile(path);

                        entry.CertificateSubject = cert.Subject;
                    }
                    catch
                    {
                        // Si falla (por falta de firma o permisos), asumimos que no tiene/no es accesible: axiomaticamente 
                        entry.CertificateSubject = "Read the notes";
                    }



                }
                catch (Exception ex)
                {
                    entry.Note = "   ";
                }

            });

            return results;

        }

        private static string GetProcessInstanceName(int pid)
        {
            var category = new PerformanceCounterCategory("Process");

            foreach (var instance in category.GetInstanceNames())
            {
                using var counter = new PerformanceCounter(
                    "Process", "ID Process", instance, true);

                if ((int)counter.RawValue == pid)
                    return instance;
            }

            return null;
        }


        private static async Task<double> GetCpuUsageAsync(int pid)
        {
            var instanceName = GetProcessInstanceName(pid);

            using var cpuCounter = new PerformanceCounter(
                "Process", "% Processor Time", instanceName, true);

            cpuCounter.NextValue();
            await Task.Delay(500);

            var value = cpuCounter.NextValue();
            return value / Environment.ProcessorCount;
        }

    }
}