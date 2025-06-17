using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Views;

/// <summary>
/// Implementation of the calculator view using Unity UI components.
/// </summary>
public class CalculatorView : MonoBehaviour, ICalculatorView
{
    [SerializeField] private TMP_InputField inputField; // Input field for user expression.
    [SerializeField] private TMP_Text resultText; // Text component for displaying output/history.
    [SerializeField] private Button resultButton; // Button to trigger calculation.

    public string InputText
    {
        get => inputField.text;
        set => inputField.text = value;
    }

    public event Action OnCalculateButtonClicked;

    private void Start()
    {
        resultText.text = "";
    }

    public void SetResult(string result)
    {
        // Append new result or message to output display with newline.
        resultText.text += "\n" + result;

        // Optionally, scroll to bottom or clear input after calculation.

        // Example: Clear input after successful calculation:
        // InputText = "";

        // Example: Scroll view handling can be added here if needed.
    }

    public void ShowError(string message)
    {
        // Append error message to output display with newline.
        resultText.text += "\n" + message;
    }

    private void OnCalculateButtonClick()
    {
        OnCalculateButtonClicked?.Invoke();
    }

    private void OnEnable()
    {
        // Subscribe button click to event.
        resultButton.onClick.AddListener(OnCalculateButtonClick);
    }
    
    private void OnDisable()
    {
        // Unsubscribe button click from event.
        resultButton.onClick.RemoveListener(OnCalculateButtonClick);
    }
}