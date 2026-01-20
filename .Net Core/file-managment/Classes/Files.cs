using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace AppFiles.Classes
{
    public static class Files
    {
        public static Dictionary<string, string> GetFileHashes(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"The directory '{directoryPath}' does not exist.");
            }

            var fileHashes = new Dictionary<string, string>();
            var files = Directory.GetFiles(directoryPath);

            using (var sha256 = SHA256.Create())
            {
                foreach (var file in files)
                {
                    using (var stream = File.OpenRead(file))
                    {
                        var hash = sha256.ComputeHash(stream);
                        var hashString = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                        fileHashes.Add(file, hashString);
                    }
                }
            }

            return fileHashes;
        }

        public static void ListFileHashes(string directoryPath)
        {
            var fileHashes = GetFileHashes(directoryPath);

            foreach (var fileHash in fileHashes)
            {
                Console.WriteLine($"File: {fileHash.Key}, Hash: {fileHash.Value}");
            }
        }

        public static List<string> GetKeysWithDuplicateValues(Dictionary<string, string> dictionary)
        {
            return dictionary
                .GroupBy(pair => pair.Value)
                .Where(group => group.Count() > 1)
                .SelectMany(group => group.Select(pair => pair.Key))
                .ToList();
        }

        public static void DeleteFilesWithDuplicateHashes(string directoryPath)
        {
            var fileHashes = GetFileHashes(directoryPath);
            var duplicateGroups = fileHashes
                .GroupBy(pair => pair.Value)
                .Where(group => group.Count() > 1);

            foreach (var group in duplicateGroups)
            {
                // Order files by creation time (oldest first)
                var filesOrderedByCreationTime = group
                    .Select(pair => new FileInfo(pair.Key))
                    .OrderBy(fileInfo => fileInfo.CreationTime)
                    .ToList();

                // Skip the oldest file and delete the rest
                foreach (var fileInfo in filesOrderedByCreationTime.Skip(1))
                {
                    try
                    {
                        File.Delete(fileInfo.FullName);
                        Console.WriteLine($"Deleted file: {fileInfo.FullName}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting file {fileInfo.FullName}: {ex.Message}");
                    }
                }
            }
        }

        public static void RenameFilesBasedOnCreationTime(string directoryPath, string category, string tags)
        {
            directoryPath = Path.GetFullPath(directoryPath);
            string newFilesDirectory = directoryPath;
            string parentFilesDirectory = Directory.GetParent(directoryPath).FullName;
            string categoryFilesDirectory = Path.Combine(parentFilesDirectory, category + " " + tags);

            var files = Directory.GetFiles(newFilesDirectory);

            if (files.Count() != 0 && !Directory.Exists(categoryFilesDirectory))
            {
                Directory.CreateDirectory(categoryFilesDirectory);
            }

            for (int i = 0; i < files.Count(); i++)
            {
                var fileInfo = new FileInfo(files[i]);
                var creationTime = fileInfo.CreationTime;
                var newFileName = $"{creationTime:[yyyy MM dd] [HH mm ss]}_{category}_[{fileInfo.Length}]_[{i}]{fileInfo.Extension}";
                var newFilePath = Path.Combine(categoryFilesDirectory, newFileName);

                try
                {
                    File.Move(files[i], newFilePath);
                    Console.WriteLine($"Renamed file: {files[i]} to {newFilePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error renaming file {files[i]} to {newFilePath}: {ex.Message}");
                }
            }
        }

        public static void WriteLog(string filePath, string  text, string category)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter( filePath, true))
                {
                    writer.WriteLine($"[   new Category: {category} new Date {DateTime.Now}   ;]");
                    writer.WriteLine();
                    writer.WriteLine($"{text}");
                    writer.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
