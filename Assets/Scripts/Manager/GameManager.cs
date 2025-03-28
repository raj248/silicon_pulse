using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }

    public void Startup()
    {
        Status = ManagerStatus.Started;
    }

    private void Start()
    {
       
    }
}
