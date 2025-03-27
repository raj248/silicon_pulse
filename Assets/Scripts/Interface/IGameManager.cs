
// ReSharper disable CheckNamespace
public interface IGameManager
{
    ManagerStatus Status { get; }

    void Startup();
}
