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
        // Subscribe button click to event.
        resultButton.onClick.AddListener(() => OnCalculateButtonClicked?.Invoke());

        // Optional: Initialize output display if needed.
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

#if UNITY_EDITOR || DEVELOPMENT_BUILD || true // For debugging purposes only!
#endif
    }

    public void ShowError(string message)
    {
        // Append error message to output display with newline.
        resultText.text += "\n" + message;
    }
}