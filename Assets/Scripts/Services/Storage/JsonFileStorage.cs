using System.IO;
using UnityEngine;

namespace Services.Storage
{
    [System.Serializable]
    class HistoryData
    {
        public string[] expressions;
        public float[] results;
    }

    /// <summary>
    /// Implements storage using JSON file in persistent data path.
    /// </summary>
    public class JsonFileStorage : IStorage
    {
        private readonly string filePath;

        public JsonFileStorage()
        {
            filePath = Path.Combine(Application.persistentDataPath, "history.json");
        }

        public void SaveHistory(string[] expressions, float[] results)
        {
            var data = new HistoryData { expressions = expressions, results = results };
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, json);
        }

        public (string[] expressions, float[] results) LoadHistory()
        {
            if (!File.Exists(filePath))
                return (new string[0], new float[0]);

            string json = File.ReadAllText(filePath);
            var data = JsonUtility.FromJson<HistoryData>(json);
            return (data.expressions, data.results);
        }
    }
}