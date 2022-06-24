using System;
using Newtonsoft.Json;

namespace Goo.Saves
{
    [Serializable]
    public abstract class SaveSerializable
    {
        private const string NoneVersion = "None";

        [JsonProperty("Ver")] public string Version { get; set; }

        [JsonIgnore] public abstract string Key { get; }
        [JsonIgnore] public virtual string LatestVersion => NoneVersion;
        [JsonIgnore] public bool Initialized => Version != null;

        public SaveSerializable()
        {
            Version = null;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void ReportLoaded()
        {
            AfterLoad();
            UpdateVersion();
        }

        protected void UpdateVersion()
        {
            if (Version != LatestVersion)
            {
                Migrations();
                Version = LatestVersion;
            }
        }

        protected virtual void Migrations()
        {
        }

        protected virtual void AfterLoad()
        {
        }
    }
}