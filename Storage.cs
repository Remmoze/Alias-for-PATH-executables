using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Diagnostics;

namespace CustomRunCommands
{
    class JsonStorage
    {
        public DateTime CreationDate { get; set; }
        public List<JsonShortcut> Shortcuts { get; set; }
    }

    class JsonShortcut
    {
        public string ShortName { get; set; }
        public string Path { get; set; }
        public DateTime CreationDate { get; set; }
    }

    public class Storage
    {
        const string StorageDirectoryPath = @"C:/PATH";
        const string StorageFilePath = StorageDirectoryPath + "/.path.json";

        private JsonStorage Data = new JsonStorage();

        public Storage()
        {
            if (!Directory.Exists(StorageDirectoryPath))
            {
                Directory.CreateDirectory(StorageDirectoryPath);
            }
            if (!File.Exists(StorageFilePath))
            {
                GenerateNewStorageFile();
                Debug.WriteLine("Generated a new storage file.");
            }
            else
            {
                LoadStorageFile();
                Debug.WriteLine("Storage file has been read.");
            }
        }

        public void AddNewShortcut(string name, string path)
        {
            Data.Shortcuts.Add(new JsonShortcut()
            {
                ShortName = name,
                Path = path
            });
            Debug.WriteLine($"Added new storage shortcut \"{name}\" to {path}");
        }

        private void Save()
        {
            File.WriteAllText(StorageFilePath, SerializeJson());
            Debug.WriteLine("Storage file has been saved.");
        }

        private string SerializeJson()
        {
            return JsonSerializer.Serialize(Data, new()
            {
                WriteIndented = true,
            });
        }

        private void GenerateNewStorageFile()
        {
            using (FileStream fs = File.Create(StorageFilePath))
            {
                Data.CreationDate = DateTime.Now;
                var info = new UTF8Encoding(true).GetBytes(SerializeJson());
                fs.Write(info, 0, info.Length);
            }
        }

        private void LoadStorageFile()
        {
            Data = JsonSerializer.Deserialize<JsonStorage>(File.ReadAllText(StorageFilePath));
        }
    }
}
