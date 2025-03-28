using UnityEngine;
using UnityEngine.UI;

public class DashButton : MonoBehaviour
{
    [SerializeField] private Button dashButton;

    private void Start()
    {
        dashButton.onClick.AddListener(OnDashPressed);
    }

    private void OnDashPressed()
    {
        // Assuming InputManager handles input events and the attack controller listens
        Managers.Input.TriggerDash();
    }
}