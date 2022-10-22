using System.Collections;
using UnityEngine;

public class LeaderboardButton : MonoBehaviour
{
    [SerializeField] private UIButtonsCotroller _buttons;
    [SerializeField] private LeaderboardView _leaderboardHandler;

    private bool _able;

    private const float _takeDownTime = 1f;

    public void OnButtonClick()
    {
        if (_able)
        {
            _leaderboardHandler.gameObject.SetActive(true);
            YandexLeaderboard.Instance?.Construct(_leaderboardHandler);
#if UNITY_WEBGL && YANDEX_GAMES && !UNITY_EDITOR
        YandexLeaderboard.Instance.FormListOfTopPlayers();
#elif UNITY_EDITOR
            YandexLeaderboard.Instance?.FormListOfTopPlayers(true);
#endif
            _able = false;
            StartCoroutine(Approve());
        }
    }

    private IEnumerator Approve()
    {
        yield return new WaitForSeconds(_takeDownTime);
        _able = true;
    }

    private void OnEnable()
    {
        _able = true;
        _buttons.LeadersButtonClick += OnButtonClick;
    }

    private void OnDisable()
    {
        _buttons.LeadersButtonClick += OnButtonClick;
    }
}
