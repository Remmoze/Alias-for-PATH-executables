using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Diagnostics;

namespace CustomRunCommands
{
    public class JsonStorage
    {
        public DateTime CreationDate { get; set; }
        public List<Shortcut> Shortcuts { get; set; }
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

        public void AddShortcut(Shortcut shortcut)
        {
            shortcut.CreationDate = DateTime.Now;
            Data.Shortcuts.Add(shortcut);
            Debug.WriteLine($"Added a new storage shortcut \"{shortcut.ShortName}\" to {shortcut.Path}");
        }

        public void Save()
        {
            File.WriteAllText(StorageFilePath, SerializeJson());
            Debug.WriteLine("Storage file has been saved.");
        }

        private string SerializeJson()
        {
            return JsonSerializer.Serialize(Data, new()
            {
                IgnoreReadOnlyProperties = true,
                WriteIndented = true,
            });
        }

        private void GenerateNewStorageFile()
        {
            using (FileStream fs = File.Create(StorageFilePath))
            {
                Data = new JsonStorage();
                Data.Shortcuts = new List<Shortcut>();
                Data.CreationDate = DateTime.Now;
                var info = new UTF8Encoding(true).GetBytes(SerializeJson());
                fs.Write(info, 0, info.Length);
            }
        }

        private void LoadStorageFile()
        {
            Data = JsonSerializer.Deserialize<JsonStorage>(File.ReadAllText(StorageFilePath));
            if(Data == null)
            {
                GenerateNewStorageFile();
                return;
            }
            if(Data.Shortcuts == null)
            {
                Data.Shortcuts = new List<Shortcut>();
            }
        }
    }
}
