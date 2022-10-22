using Agava.YandexGames;
using UnityEngine;

public class Rewarded : MonoBehaviour
{
    [SerializeField] private CoinCollector _player;

    public void ShowRewarded(int mululpy = 1)
    {
        VideoAd.Show(onOpenCallback: Mute, onCloseCallback: UnMute, onRewardedCallback: Reward);
    }

    private void Reward()
    {
        UnMute();
        _player.MultyplyCoins();
    }

    private void Mute()
    {
        AudioListener.volume = 0;
    }

    private void UnMute()
    {
        if (Muter.Instance.Muted == false)
            AudioListener.volume = 1;
    }
}