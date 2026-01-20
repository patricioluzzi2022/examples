using services.Classes.NetStat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Windows;

namespace services.Classes
{
    public class WindowService
    {
        private ServiceController Service;

        public WindowServiceInfo ServiceInfo { get; set; }

        public WindowService(ServiceController service) {
            Service = service;
            LoadServiceInfo();
        }

        public void LoadServiceInfo()
        {
            ServiceInfo = new WindowServiceInfo();
            ServiceInfo.ServiceName = Service.ServiceName;
            ServiceInfo.DisplayName = Service.DisplayName;
            ServiceInfo.Status = Service.Status;
            ServiceInfo.CanStop = Service.CanStop;
        }

        public void Start() {
            if (Service.Status == ServiceControllerStatus.Stopped)
            {
                
                try
                {
                    Service.Start();
                    Service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(20));
                    ServiceInfo.Status = Service.Status;
                    ServiceInfo.CanStop = Service.CanStop;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al iniciar servicio: " + ex.Message);
                }
                
            }
        }

        public void Stop()
        {
            if (Service.Status == ServiceControllerStatus.Running & Service.CanStop)
            {
                try
                {
                    Service.Refresh();
                    Service.Stop();
                    Service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(20));
                    ServiceInfo.Status = Service.Status;
                    ServiceInfo.CanStop = Service.CanStop;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al detener servicio: " + ex.Message);
                }

            }
        }

        public async void ShowDetails() {
            string query = $"SELECT * FROM Win32_Service WHERE Name = '{ServiceInfo.ServiceName}'";
            using (var searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject mo in searcher.Get())
                {
                    ServiceInfo.Description = (string)mo["Description"];
                    ServiceInfo.ProcessId = Convert.ToInt32(mo["ProcessId"]);
                    ServiceInfo.StartMode = (string)mo["StartMode"]; // "Auto", "Manual", "Disabled"
                    ServiceInfo.PathName = (string)mo["PathName"];
                    break;
                }
            }

            ServiceInfo.NetStat = await NetStatClass.GetNetStatusByService(ServiceInfo);

            if(ServiceInfo.NetStat.Count != 0)
            {
                ServiceInfo.LastUpdate = DateTime.Now;
            }
        }

        public void RefreshDetails() {
            ShowDetails();
        }

    }

}
