using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntergrationMetric : MonoBehaviour
{
    public static IntergrationMetric Instance { get; private set; }

    public int SessionCount;

    private const string SessionCountName = "sessionCount";

    private void Start()
    {
        GameAnalytics.Initialize();
        Instance = this;

        OnGameStart();
    }

    public void OnGameStart()
    {
        Dictionary<string, object> count = new Dictionary<string, object>();
        count.Add("count", CountSession());
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game_start", count);
    }

    public void OnLevelStart(int levelIndex)
    {
        var levelProperty = CreateLevelProperty(levelIndex);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level_start", levelProperty);
    }

    public void OnLevelComplete(int levelComplitioTime, int levelIndex)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "level", levelIndex }, { "time_spent", levelComplitioTime } };

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level_complete", userInfo);
    }

    public void OnLevelFail(int levelFailTime, int levelIndex)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "level", levelIndex }, { "time_spent", levelFailTime } };

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "fail", userInfo);
    }

    public void OnRestartLevel(int levelIndex)
    {
        var levelProperty = CreateLevelProperty(levelIndex);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "restart", levelProperty);
    }

    public void OnRewardedAdStarted()
    {
        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, "rewarded_start", "button");
    }

    public void OnRewardedAdEnded()
    {
        GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.RewardedVideo, "rewarded_shown", "button");
    }

    public void OnInterstitialShown()
    {
        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, "interstitial_start", "end of the level");
    }

    public void OnSoftSpent(int price, string itemType, string codeName)
    {
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "coins", price, itemType, codeName);
    }

    private Dictionary<string, object> CreateLevelProperty(int levelIndex)
    {
        Dictionary<string, object> level = new Dictionary<string, object>();
        level.Add("level", levelIndex);

        return level;
    }

    private int CountSession()
    {
        int count = 1;

        if (PlayerPrefs.HasKey(SessionCountName))
        {
            count = PlayerPrefs.GetInt(SessionCountName);
            count++;
        }

        PlayerPrefs.SetInt(SessionCountName, count);
        SessionCount = count;

        return count;
    }
}