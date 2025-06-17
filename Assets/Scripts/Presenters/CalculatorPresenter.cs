using System;
using System.Collections.Generic;
using Models;
using Services.Storage;
using Views;

namespace Presenters
{
    /// <summary>
    /// The Presenter manages interactions between the View and Model,
    /// handles business logic and data persistence.
    /// </summary>
    public class CalculatorPresenter
    {
        private readonly ICalculatorView view;          // Reference to UI view.
        private readonly ICalculatorModel model;       // Business logic model.
        private readonly IStorage storage;             // Data persistence.

        private readonly List<string> historyExpressions = new();  // List of past expressions.
        private readonly List<float> historyResults = new();       // Corresponding results.

        /// <summary>
        /// Constructor initializes dependencies and loads history.
        /// Subscribes to view events.
        /// </summary>
        /// <param name="view">UI view instance.</param>
        /// <param name="model">Business logic model.</param>
        /// <param name="storage">Persistence storage.</param>
        public CalculatorPresenter(ICalculatorView view, ICalculatorModel model, IStorage storage)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.model = model ?? throw new ArgumentNullException(nameof(model));
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));

            // Subscribe to button click event from the view.
            this.view.OnCalculateButtonClicked += OnCalculateClicked;

            LoadHistory();  // Load previous calculations from storage on startup.
        }

        /// <summary>
        /// Loads saved history from storage and displays it.
        /// </summary>
        private void LoadHistory()
        {
            var data = storage.LoadHistory();
            for (int i=0; i<data.expressions.Length; i++)
            {
                historyExpressions.Add(data.expressions[i]);
                historyResults.Add(data.results[i]);
                view.SetResult($"{data.expressions[i]} = {data.results[i]}");
            }
        }

        /// <summary>
        /// Saves current history to persistent storage.
        /// </summary>
        private void SaveHistory()
        {
            storage.SaveHistory(historyExpressions.ToArray(), historyResults.ToArray());
        }

        /// <summary>
        /// Handles calculation when user clicks "Result".
        /// Validates input, performs calculation via model,
        /// updates UI or shows error.
        /// </summary>
        private void OnCalculateClicked()
        {
            string input = view.InputText.Trim();

            if (string.IsNullOrEmpty(input))
                return;

            var result = model.Calculate(input);

            if (result.HasValue)
            {
                string resStr = result.Value.ToString();

                // Display calculation result in output area.
                view.SetResult($"{input} = {resStr}");

                // Save to history lists and persist data.
                historyExpressions.Add(input);
                historyResults.Add(result.Value);
                SaveHistory();

                // Optional: clear input after success:
                // view.InputText = "";
            }
            else
            {
                // Show error message if calculation failed or invalid input detected.
                view.ShowError("Error");
         
                // Optional: show more detailed error messages or handle specific cases here.
            }
        }
    }
}