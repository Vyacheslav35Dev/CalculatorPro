using System;
using System.Collections.Generic;
using Services.Storage;

namespace Services
{
    public class CalculatorHistoryManager
    {
        private readonly IStorage storage;
        private readonly List<string> expressions = new List<string>();
        private readonly List<float> results = new List<float>();

        public Action OnUpdate;

        public CalculatorHistoryManager(IStorage storage)
        {
            this.storage = storage;
            LoadHistory();
        }

        public void AddEntry(string expression, float result)
        {
            expressions.Add(expression);
            results.Add(result);
            SaveHistory();
        }

        private void SaveHistory()
        {
            storage.SaveHistory(expressions.ToArray(), results.ToArray());
        }

        private void LoadHistory()
        {
            var loadedData = storage.LoadHistory();
            expressions.Clear();
            results.Clear();

            expressions.AddRange(loadedData.expressions);
            results.AddRange(loadedData.results);
        
            OnUpdate?.Invoke();
        }
    }
}