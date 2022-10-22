using Agava.YandexGames;
using UnityEngine;
using System.Collections.Generic;

public class YandexLeaderboard : MonoBehaviour
{
    private LeaderboardView _leaderboardView;

    private const string _leaderboardName = "leaders";
    public static YandexLeaderboard Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Construct(LeaderboardView leaderboard)
    {
        _leaderboardView = leaderboard;
    }

    public void FormListOfTopPlayers(bool test = false)
    {
        List<PlayerInfoLeaderboard> top5Players = new List<PlayerInfoLeaderboard>();

        if (test)
        {
            for (int i = 0; i < 5; i++)
            {
                top5Players.Add(new PlayerInfoLeaderboard("name", i));
            }

            _leaderboardView.ConstructLeaderboard(top5Players);

            return;
        }

        Leaderboard.GetEntries(_leaderboardName, (result) =>
        {
            Debug.Log($"My rank = {result.userRank}");

            int resultsAmount = result.entries.Length;

            resultsAmount = Mathf.Clamp(resultsAmount, 1, 5);

            for (int i = 0; i < resultsAmount; i++)
            {
                string name = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = "Anonymos";

                int score = result.entries[i].score;

                top5Players.Add(new PlayerInfoLeaderboard(name, score));
            }

            _leaderboardView.ConstructLeaderboard(top5Players);
        });
    }

    public void AddPlayerToLeaderboard(int score)
    {
#if !UNITY_EDITOR
        if (!PlayerAccount.IsAuthorized)
            return;

        Leaderboard.SetScore(_leaderboardName, score);
#endif
    }
}

public class PlayerInfoLeaderboard
{
    public string Name { get; private set; }
    public int Score { get; private set; }

    public PlayerInfoLeaderboard(string name, int score)
    {
        Name = name;
        Score = score;
    }
}
