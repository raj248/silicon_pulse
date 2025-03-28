using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }
    public static UIManager Instance { get; private set; }

    private readonly Dictionary<string, GameObject> _uiElements = new();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Startup()
    {
        Status = ManagerStatus.Started;

    }

    /// <summary>
    /// Register a UI element with a key for quick control
    /// </summary>
    public void RegisterUIElement(string key, GameObject element)
    {
        if (!_uiElements.ContainsKey(key))
            _uiElements.Add(key, element);
    }

    /// <summary>
    /// Show a specific UI element
    /// </summary>
    public void Show(string key)
    {
        if (_uiElements.TryGetValue(key, out var element))
            element.SetActive(true);
    }

    /// <summary>
    /// Hide a specific UI element
    /// </summary>
    public void Hide(string key)
    {
        if (_uiElements.TryGetValue(key, out var element))
            element.SetActive(false);
        Debug.Log("Hiding "+ key);
    }

    /// <summary>
    /// Toggle visibility of a specific UI element
    /// </summary>
    public void Toggle(string key)
    {
        if (_uiElements.TryGetValue(key, out var element))
            element.SetActive(!element.activeSelf);
    }


    /// <summary>
    /// Hide all registered UI elements
    /// </summary>
    public void HideAll()
    {
        foreach (var element in _uiElements.Values)
            element.SetActive(false);
    }
}