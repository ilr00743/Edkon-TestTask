using System.IO;
using UnityEngine;

namespace Services.SaveLoad
{
    public class SaveLoadService: ISaver, ILoader
    {
        private static SaveLoadService _instance;

        private SaveLoadService()
        {
        }
        
        public static SaveLoadService GetInstance()
        {
            return _instance ??= new SaveLoadService();
        }
        
        public void Save<T>(string key, T data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(GetFilePath(key), json);
            Debug.Log($"Saved data with key '{key}' to {GetFilePath(key)}");
        }
        
        public T Load<T>(string key)
        {
            string filePath = GetFilePath(key);
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonUtility.FromJson<T>(json);
            }

            Debug.LogWarning($"No save file found for key '{key}' at {filePath}");
            return default;
        }
        
        public void Delete(string key)
        {
            string filePath = GetFilePath(key);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log($"Deleted save file for key '{key}' at {filePath}");
            }
        }
        
        public bool HasSave(string key)
        {
            return File.Exists(GetFilePath(key));
        }
        
        private string GetFilePath(string key)
        {
            return Path.Combine(Application.persistentDataPath, $"{key}.json");
        }
    }
}