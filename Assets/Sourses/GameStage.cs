using UnityEngine;
using UnityEngine.Events;

public class GameStage : MonoBehaviour
{
    public event UnityAction GameStarted;
    public event UnityAction GameStop;

    public static GameStage Instance { get; private set; }

    public void StartGame() => GameStarted?.Invoke();

    public void StopGame() => GameStop?.Invoke();

    private void Awake()
    {
        Instance = this;
    }
}
