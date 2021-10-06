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
                using (FileStream fs = File.Create(StorageFilePath))
                {
                    var data = JsonSerializer.Serialize(Data, new()
                    {
                        WriteIndented = true,
                    });
                    var info = new UTF8Encoding(true).GetBytes(data);
                    fs.Write(info, 0, info.Length);
                }
                Console.WriteLine("Generated new storage file.");
            }
            //LoadData();
        }

        //private void LoadData()
        //{
        //    var data = JObject.Parse(File.ReadAllText(StorageFilePath));
        //    foreach (JValue shortcut in (JArray)data["shortcuts"])
        //    {
        //        Shortcuts.Add(new Shortcut(shortcut));
        //    }
        //}
    }
}
