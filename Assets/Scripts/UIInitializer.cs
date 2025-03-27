using UnityEngine;
public class UIInitializer : MonoBehaviour
{
    [SerializeField] private GameObject dashButton;
    [SerializeField] private GameObject interactionButton;

    private void Start()
    {
        UIManager.Instance.RegisterUIElement("Dash", dashButton);
        UIManager.Instance.RegisterUIElement("Interact", interactionButton);

        // Start with only dash visible
        UIManager.Instance.Show("Dash");
        UIManager.Instance.Hide("Interact");
    }
}