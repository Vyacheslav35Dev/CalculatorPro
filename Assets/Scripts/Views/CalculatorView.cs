using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Views;
using Button = UnityEngine.UI.Button;

/// <summary>
/// Implementation of the calculator view using Unity UI components.
/// Handles user input, displays results, and manages user interactions.
/// </summary>
public class CalculatorView : MonoBehaviour, ICalculatorView
{
    [Header("UI Components")]
    [SerializeField] 
    private TMP_InputField inputField; // Input field for user to enter expressions.
    [SerializeField] 
    private TMP_Text resultText;       // Text component for displaying calculation results and history.
    [SerializeField] 
    private Button resultButton; // Button that triggers the calculation process.
    [SerializeField] 
    private ScrollRect scrollRect;     // ScrollRect component for scrolling behavior.


    /// <summary>
    /// Gets or sets the current text in the input field.
    /// </summary>
    public string InputText
    {
        get => inputField.text;
        set => inputField.text = value;
    }

    /// <summary>
    /// Event invoked when the user clicks the "Calculate" button.
    /// </summary>
    public event Action OnCalculateButtonClicked;

    private void Start()
    {
        // Initialize the output display to be empty at startup.
        resultText.text = "";
    }

    /// <summary>
    /// Appends a calculation result or message to the output display.
    /// </summary>
    /// <param name="result">The string message or calculation result to display.</param>
    public void SetResult(string result)
    {
        // Append the new result/message with a newline for readability.
        InputText = "";
        resultText.text += "\n" + result;
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    /// <summary>
    /// Appends an error message to the output display.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    public void ShowError(string message)
    {
        // Append error message with newline for clarity.
        resultText.text += "\n" + message;
    }

    /// <summary>
    /// Handler for button click event; invokes the calculation event.
    /// </summary>
    private void OnCalculateButtonClick()
    {
        OnCalculateButtonClicked?.Invoke();
    }

    private void OnEnable()
    {
        // Subscribe to button click event when the object is enabled.
        resultButton.onClick.AddListener(OnCalculateButtonClick);
    }
    
    private void OnDisable()
    {
        // Unsubscribe from button click event when the object is disabled to prevent memory leaks.
        resultButton.onClick.RemoveListener(OnCalculateButtonClick);
    }
}