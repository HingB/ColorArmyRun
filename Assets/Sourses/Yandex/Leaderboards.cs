using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class Leaderboards : MonoBehaviour
{
    [SerializeField] private CoinCollector _coins;
    [SerializeField] private Leader[] _leaders = new Leader[5];

    public void ShowLeaders()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        PlayerAccount.RequestPersonalProfileDataPermission();
        if (PlayerAccount.IsAuthorized)
            LoadLeader();
        else
            PlayerAccount.Authorize(onSuccessCallback:LoadLeader);
#endif
    }

    private void LoadLeader()
    {
        Leaderboard.GetEntries("leaders", (result) =>
        {
            int id = 0;

            foreach (var entry in result.entries)
            {
                string name = entry.player.publicName;
                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";
                if (id >= _leaders.Length)
                    return;

                _leaders[id].Fill(name, entry.score.ToString());
                id++;
            }
        });
    }

    public void SetLeaderScore(int newScore)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Leaderboard.GetPlayerEntry("leaders", (result) =>
        {
            if (result != null && result.score > newScore)
                return;
                  
            Leaderboard.SetScore("leaders", newScore);
        });
#endif
    }

    private void OnEnable()
    {
        _coins.OnCoinsValueChanged += SetLeaderScore;
    }

    private void OnDisable()
    {
        _coins.OnCoinsValueChanged -= SetLeaderScore;
    }
}