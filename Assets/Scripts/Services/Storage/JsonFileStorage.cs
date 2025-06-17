using System;
using System.IO;
using UnityEngine;

namespace Services.Storage
{
    /// <summary>
    /// Serializable class representing the structure of stored history data,
    /// including calculation expressions and their results.
    /// </summary>
    [System.Serializable]
    class HistoryData
    {
        public string currentExpression; // Currently entered calculation expression.
        public string[] expressions; // Array of saved calculation expressions.
        public string[] results;     // Corresponding array of calculation results.
    }

    /// <summary>
    /// Implements storage functionality by saving and loading history data
    /// to a JSON file located in the application's persistent data path.
    /// </summary>
    public class JsonFileStorage : IStorage
    {
        private readonly string _filePath; // Full path to the JSON storage file.

        /// <summary>
        /// Initializes a new instance of JsonFileStorage, setting the file path.
        /// <param name="nameFile">Name save file.</param>
        /// </summary>
        public JsonFileStorage(string nameFile = "history.json")
        {
            // Combine persistent data path with filename for storage location.
            _filePath = Path.Combine(Application.persistentDataPath, nameFile);
        }

        /// <summary>
        /// Saves the provided expressions and results to a JSON file.
        /// </summary>
        /// <param name="expressions">Array of calculation expressions as strings.</param>
        /// <param name="results">Array of corresponding calculation results as strings.</param>
        public void SaveHistory(string[] expressions, string[] results, string currentExpression)
        {
            try
            {
                var data = new HistoryData { expressions = expressions, results = results, currentExpression = currentExpression };
                string json = JsonUtility.ToJson(data); // Serialize data to JSON format.
                File.WriteAllText(_filePath, json);      // Write JSON string to file.
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Loads calculation history from the JSON file.
        /// If the file does not exist, returns empty arrays.
        /// </summary>
        /// <returns>A tuple containing arrays of expressions and their corresponding results.</returns>
        public (string[] expressions, string[] results, string currentExpression) LoadHistory()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return (new string[0], new string[0], ""); // Return empty arrays if no data exists.

                string json = File.ReadAllText(_filePath); // Read JSON content from file.
                var data = JsonUtility.FromJson<HistoryData>(json); // Deserialize JSON to HistoryData object.
                return (data.expressions, data.results, data.currentExpression); // Return stored expressions and results.
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}