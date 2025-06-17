using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the display of a message view with a close button.
/// Handles showing and hiding the message panel.
/// </summary>
public class MessageView : MonoBehaviour
{
    [Header("Close Button")]
    [SerializeField]
    private Button closeButton; // Reference to the UI Button used to close the message view.

    /// <summary>
    /// Shows the message view by activating its GameObject.
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the message view by deactivating its GameObject.
    /// </summary>
    private void Close()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Subscribes to the close button click event when the object is enabled.
    /// </summary>
    private void OnEnable()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(Close);
        }
        else
        {
            Debug.LogWarning("Close Button is not assigned in the inspector.");
        }
    }

    /// <summary>
    /// Unsubscribes from the close button click event when the object is disabled.
    /// </summary>
    private void OnDisable()
    {
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(Close);
        }
    }
}