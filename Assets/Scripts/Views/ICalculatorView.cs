using System;

namespace Views
{
    /// <summary>
    /// Interface for the calculator UI view.
    /// </summary>
    public interface ICalculatorView
    {
        string InputText { get; set; } // User input field text.

        void SetResult(string result); // Append result or message to output display.

        void ShowError(string message); // Show error message.

        event Action OnCalculateButtonClicked; // Event triggered when user presses "Result" button.
    }
}