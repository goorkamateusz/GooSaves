using System;
using System.Collections;
using UnityEngine;
using Goo.Tools.Patterns;

namespace Goo.Saves
{
    public class SaveManager : SceneSingleton<SaveManager>
    {
        // todo clear saves API
        public event Action PreSave;

        [SerializeField] private SaveFileProvider _file = new SaveFileProvider();

        private Save Save { get; set; }

        public bool IsLoaded => Save != null;

        public T Load<T>(T obj) where T : SaveSerializable
        {
            string key = obj.Key;
            if (!Save.ContainsKey(key))
                Save[key] = obj;
            Save[key].ReportLoaded();
            return Save[key] as T;
        }

        public void Load<T>(ref T obj) where T : SaveSerializable
        {
            obj = Load<T>(obj);
        }

        private void LoadFile()
        {
            Save = _file.Load();
        }

        private void SaveFile()
        {
            PreSave?.Invoke();
            _file.Save(Save);
        }

        protected override void OnAwake()
        {
            LoadFile();
        }

        protected void OnApplicationQuit()
        {
            SaveFile();
        }

        public static IEnumerator WaitUntilGameLoaded()
        {
            while (NotInitialized)
                yield return null;
            while (!Instance.IsLoaded)
                yield return null;
        }

#if UNITY_EDITOR
        public SaveFileProvider GetFileProvider() => _file;
#endif
    }
}
