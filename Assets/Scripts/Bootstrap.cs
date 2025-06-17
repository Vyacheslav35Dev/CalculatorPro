using Models;
using Presenters;
using Services.Storage;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Calculator View")]
    [SerializeField]
    public CalculatorView calculatorView;
    private CalculatorPresenter _presenter;

    void Start()
    {
        var model = new CalculatorModel();
        IStorage storage = new JsonFileStorage();
        
        // Create presenter and connect all parts:
        _presenter = new CalculatorPresenter(calculatorView, model, storage);
        _presenter.LoadHistory();
    }
    
    void OnApplicationQuit()
    {
        _presenter.SaveHistory();
    }
}