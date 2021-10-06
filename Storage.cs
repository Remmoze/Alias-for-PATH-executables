using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace CustomRunCommands
{
    class JsonStorage
    {
        public DateTime CreationDate { get; set; }
        public List<Shortcut> Shortcuts { get; set; }
    }

    class Shortcut
    {
        public string ShortName { get; }
        public string Path { get; }
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
                Console.WriteLine("Generated a new storage file.");
            }
            else
            {
                LoadStorageFile();
                Console.WriteLine("Storage file has been read.");
            }
        }

        private void GenerateNewStorageFile()
        {
            using (FileStream fs = File.Create(StorageFilePath))
            {
                Data.CreationDate = DateTime.Now;
                var data = JsonSerializer.Serialize(Data, new()
                {
                    WriteIndented = true,
                });
                var info = new UTF8Encoding(true).GetBytes(data);
                fs.Write(info, 0, info.Length);
            }
        }

        private void LoadStorageFile()
        {
            Data = JsonSerializer.Deserialize<JsonStorage>(File.ReadAllText(StorageFilePath));
        }
    }
}
