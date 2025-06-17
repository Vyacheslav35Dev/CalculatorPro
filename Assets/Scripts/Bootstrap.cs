using Models;
using Presenters;
using Services;
using Services.Storage;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Calculator View")]
    [SerializeField]
    public CalculatorView calculatorView;

    void Start()
    {
        var model = new CalculatorModel();
        IStorage storage = new JsonFileStorage();
        
        // Create presenter and connect all parts:
        var presenter = new CalculatorPresenter(calculatorView, model, storage);
        CalculatorHistoryManager historyManager = new CalculatorHistoryManager(storage);

// Когда пользователь вводит выражение и получает результат:
        string expression = "54+21";
        float result = 75; // например, результат вычислений

// Добавьте в историю:
        historyManager.AddEntry(expression, result);
    }
    
    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}