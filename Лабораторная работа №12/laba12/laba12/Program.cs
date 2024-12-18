using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace laba12
{
    internal class UVVLog
    {
        private static string logFile = "uvvlogfile.txt";

        public static void WriteLog(string action, string details)
        {
            try
            {
                string logEntry = $"{DateTime.Now}: {action} - {details}";
                File.AppendAllText(logFile, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ошибка записи в log: {ex.Message}");
            }
        }

        public static void ReadLog()
        {
            try
            {
                string[] logs = File.ReadAllLines(logFile);
                foreach (var log in logs)
                    Console.WriteLine(log);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ошибка чтения log: {ex.Message}");
            }
        }

        public static void SearchLog(string keyword)
        {
            try
            {
                var logs = File.ReadAllLines(logFile)
                               .Where(log => log.Contains(keyword));
                foreach (var log in logs)
                    Console.WriteLine(log);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ошибка поиска в log: {ex.Message}");
            }
        }
    }

    internal class UVVDiskInfo
    {
        public static void GetDiskInfo()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    Console.WriteLine($"имя диска: {drive.Name}");
                    Console.WriteLine($"тип диска: {drive.DriveType}");
                    Console.WriteLine($"общий размер: {drive.TotalSize} байт");
                    Console.WriteLine($"свободное место: {drive.TotalFreeSpace} байт");
                    Console.WriteLine($"метка тома: {drive.VolumeLabel}");
                    Console.WriteLine(new string('-', 40));
                }
            }
        }
    }

    internal class UVVFileInfo
    {
        public static void GetFileInfo(string filePath)
        {
            try
            {
                var fileInfo = new FileInfo(filePath);
                if (!fileInfo.Exists)
                {
                    Console.WriteLine("файл не существует");
                    return;
                }

                Console.WriteLine($"полный путь: {fileInfo.FullName}");
                Console.WriteLine($"размер: {fileInfo.Length} байт");
                Console.WriteLine($"расширение: {fileInfo.Extension}");
                Console.WriteLine($"имя: {fileInfo.Name}");
                Console.WriteLine($"дата создания: {fileInfo.CreationTime}");
                Console.WriteLine($"последнее изменение: {fileInfo.LastWriteTime}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ошибка получения информации о файле: {ex.Message}");
            }
        }
    }

    internal class UVVDirInfo
    {
        public static void GetDirInfo(string dirPath)
        {
            try
            {
                var dirInfo = new DirectoryInfo(dirPath);
                if (!dirInfo.Exists)
                {
                    Console.WriteLine("директория не существует");
                    return;
                }

                Console.WriteLine($"имя директории: {dirInfo.Name}");
                Console.WriteLine($"дата создания: {dirInfo.CreationTime}");
                Console.WriteLine($"количество файлов: {dirInfo.GetFiles().Length}");
                Console.WriteLine($"количество поддиректорий: {dirInfo.GetDirectories().Length}");
                Console.WriteLine("родительские директории: ");
                var parent = dirInfo.Parent;
                while (parent != null)
                {
                    Console.WriteLine($"- {parent.Name}");
                    parent = parent.Parent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ошибка получения информации о директории: {ex.Message}");
            }
        }
    }

    internal class UVVFileManager
    {
        public static void ManageFiles(string sourceDir, string fileExtension)
        {
            try
            {
                string inspectDir = Path.Combine(sourceDir, "UVVInspect");
                Directory.CreateDirectory(inspectDir);

                string logFile = Path.Combine(inspectDir, "uvvdirinfo.txt");
                File.WriteAllText(logFile, $"информация о директории: {sourceDir}");

                string filesDir = Path.Combine(sourceDir, "UVVFiles");
                Directory.CreateDirectory(filesDir);

                foreach (var file in Directory.GetFiles(sourceDir, $"*{fileExtension}"))
                {
                    string destFile = Path.Combine(filesDir, Path.GetFileName(file));
                    File.Copy(file, destFile);
                }

                string archivePath = Path.Combine(inspectDir, "UVVFiles.zip");
                ZipFile.CreateFromDirectory(filesDir, archivePath);

                string extractPath = Path.Combine(sourceDir, "UVVExtract");
                ZipFile.ExtractToDirectory(archivePath, extractPath);

                Console.WriteLine("операции с файлами выполнены успешно");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ошибка управления файлами: {ex.Message}");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            UVVLog.WriteLog("программа запущена", "начало выполнения метода Main");
            UVVLog.ReadLog();

            UVVDiskInfo.GetDiskInfo();

            UVVFileInfo.GetFileInfo("a.txt");

            UVVDirInfo.GetDirInfo("C:\\Users");

            UVVFileManager.ManageFiles("C:\\TestDir", ".txt");

            UVVLog.WriteLog("программа завершена", "выполнение метода Main завершено");
        }
    }
}
