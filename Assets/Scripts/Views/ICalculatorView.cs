using System;

namespace Views
{
    /// <summary>
    /// Interface defining the contract for the calculator UI view.
    /// Specifies properties, methods, and events for interacting with the calculator interface.
    /// </summary>
    public interface ICalculatorView
    {
        /// <summary>
        /// Gets or sets the text in the user input field.
        /// </summary>
        string InputText { get; set; }

        /// <summary>
        /// Appends a calculation result or message to the output display.
        /// </summary>
        /// <param name="result">The string containing the result or message to display.</param>
        void SetResult(string result);

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="message">The error message to show.</param>
        void ShowError(string message);

        /// <summary>
        /// Event triggered when the user clicks the "Result" button.
        /// Subscribers can handle this event to perform calculations or other actions.
        /// </summary>
        event Action OnCalculateButtonClicked;
    }
}