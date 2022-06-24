using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Goo.Saves
{
    [Serializable]
    public class SaveFileProvider
    {
        private const string DEFAULT_NAME = "save.json";

        public string Path => $"{Application.persistentDataPath}/{_fileName}";

        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
#if UNITY_EDITOR
            Formatting = Formatting.Indented,
#endif
        };

        [SerializeField] private string _fileName = DEFAULT_NAME;

        public Save Load()
        {
            return Load(Path);
        }

        public Save Load(string path)
        {
            if (File.Exists(path))
            {
                string json = Decode(File.ReadAllText(path));
                var save = JsonConvert.DeserializeObject<Save>(json, _settings);
                if (save != null)
                    return save;
            }
            return new Save();
        }

        public void Save(Save save)
        {
            Save(save, Path);
        }

        public void Save(Save save, string path)
        {
            string json = JsonConvert.SerializeObject(save, _settings);
            File.WriteAllText(path, Encode(json));
        }

        public void Delete()
        {
            Delete(Path);
        }

        public static void Delete(string path)
        {
            if (Exist(path))
                File.Delete(path);
        }

        public bool Exist()
        {
            return Exist(Path);
        }

        public static bool Exist(string path)
        {
            return File.Exists(path);
        }

        protected virtual string Encode(string json)
        {
            return json;
        }

        protected virtual string Decode(string content)
        {
            return content;
        }
    }
}