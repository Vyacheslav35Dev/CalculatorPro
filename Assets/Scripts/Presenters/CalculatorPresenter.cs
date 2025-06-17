using System;
using System.Collections.Generic;
using Models;
using Services.Storage;
using Views;

namespace Presenters
{
    /// <summary>
    /// The Presenter manages interactions between the View and Model,
    /// handles business logic, and manages data persistence.
    /// </summary>
    public class CalculatorPresenter
    {
        private readonly ICalculatorView view;          // Reference to the UI view.
        private readonly ICalculatorModel model;        // Business logic model.
        private readonly IStorage storage;              // Data persistence interface.

        private readonly List<string> historyExpressions = new();  // List to store past expressions.
        private readonly List<string> historyResults = new();      // List to store corresponding results.

        /// <summary>
        /// Constructor initializes dependencies, loads previous history,
        /// and subscribes to view events.
        /// </summary>
        /// <param name="view">Instance of the UI view.</param>
        /// <param name="model">Instance of the calculator logic model.</param>
        /// <param name="storage">Instance of the storage for data persistence.</param>
        public CalculatorPresenter(ICalculatorView view, ICalculatorModel model, IStorage storage)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.model = model ?? throw new ArgumentNullException(nameof(model));
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));

            // Subscribe to the event triggered when the user clicks the "Calculate" button.
            this.view.OnCalculateButtonClicked += OnCalculateClicked;

            LoadHistory();  // Load previous calculation history from storage upon startup.
        }

        /// <summary>
        /// Loads saved calculation history from persistent storage
        /// and displays it in the view.
        /// </summary>
        private void LoadHistory()
        {
            var data = storage.LoadHistory();
            for (int i = 0; i < data.expressions.Length; i++)
            {
                // Add each saved expression and result to local history lists.
                historyExpressions.Add(data.expressions[i]);
                historyResults.Add(data.results[i].ToString());

                // Display each historical calculation in the view.
                view.SetResult($"{data.expressions[i]} = {data.results[i]}");
            }
        }

        /// <summary>
        /// Saves current calculation history to persistent storage.
        /// </summary>
        private void SaveHistory()
        {
            storage.SaveHistory(historyExpressions.ToArray(), historyResults.ToArray());
        }

        /// <summary>
        /// Handles calculation when the user clicks the "Result" button.
        /// Validates input, performs calculation via the model,
        /// updates UI with result or error message, and updates history.
        /// </summary>
        private void OnCalculateClicked()
        {
            string input = view.InputText.Trim();

            if (string.IsNullOrEmpty(input))
                return; // Do nothing if input is empty.

            var result = model.Calculate(input);

            if (result.HasValue)
            {
                string resStr = result.Value.ToString();

                // Display the calculation result in the output area.
                view.SetResult($"{input} = {resStr}");

                // Add successful calculation to history and save it.
                historyExpressions.Add(input);
                historyResults.Add(resStr);
                SaveHistory();

                // Optionally clear input after successful calculation.
                view.InputText = "";
            }
            else
            {
                // Show error message if calculation failed or input was invalid.
                view.SetResult($"{input} = Error");

                // Record error in history for consistency.
                historyExpressions.Add(input);
                historyResults.Add("Error");
                SaveHistory();
            }
        }
    }
}