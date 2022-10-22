using UnityEngine;

public class LeaderboardButton : MonoBehaviour
{
    [SerializeField] private UIButtonsCotroller _buttons;
    [SerializeField] private LeaderboardView _leaderboardHandler;

    public void OnButtonClick()
    {
        _leaderboardHandler.gameObject.SetActive(true);
        YandexLeaderboard.Instance?.Construct(_leaderboardHandler);
#if UNITY_WEBGL && !UNITY_EDITOR
        YandexLeaderboard.Instance.FormListOfTopPlayers();
#elif UNITY_EDITOR
        YandexLeaderboard.Instance?.FormListOfTopPlayers(true);
#endif
    }

    private void OnEnable()
    {
        _buttons.LeadersButtonClick += OnButtonClick;
    }

    private void OnDisable()
    {
        _buttons.LeadersButtonClick += OnButtonClick;
    }
}
