using System;
using System.Collections;
using UnityEngine;
public class UIInitializer : MonoBehaviour
{
    [SerializeField] private GameObject dashButton;
    [SerializeField] private GameObject gameOverPanel;

    private IEnumerator WaitForUIManager()
    {
        while (!Managers.IsInitialized) yield return null;

        UIManager.Instance.RegisterUIElement("Dash", dashButton);
        UIManager.Instance.RegisterUIElement("GameOverPanel",gameOverPanel);


        // Start with only dash visible
        UIManager.Instance.Show("Dash");
        UIManager.Instance.Hide("GameOverPanel");
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForUIManager());
    }
}