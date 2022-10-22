using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BonusLevelReferee : MonoBehaviour
{
    [SerializeField] private Wall _wall;
    [SerializeField] private EnemyPool _pool;

    public event UnityAction Won;
    public event UnityAction Lose;

    private int _currentLevel => SceneManager.GetActiveScene().buildIndex;
    private float _time;

    private void OnPowerBalanceChanged(int value)
    {
        if (value <= 0)
            Win();
    }

    private void Win()
    {
        Won?.Invoke();
        GameStage.Instance.StopGame();
#if VK_GAMES
        IntergrationMetric.Instance.OnLevelComplete((int)_time, _currentLevel);
#endif
    }

    private void OnWallFall()
    {
        Lose?.Invoke();
        _pool.StopAllEnemys();
        GameStage.Instance.StopGame();
#if VK_GAMES
        IntergrationMetric.Instance.OnLevelFail((int)_time, _currentLevel);
#endif
    }

    private void Update()
    {
        _time += Time.deltaTime;
    }

    private void OnEnable()
    {
        _pool.PowerBalanceChanged += OnPowerBalanceChanged;
        _wall.Died += OnWallFall;
    }

    private void OnDisable()
    {
        _pool.PowerBalanceChanged -= OnPowerBalanceChanged;
        _wall.Died -= OnWallFall;
    }
}