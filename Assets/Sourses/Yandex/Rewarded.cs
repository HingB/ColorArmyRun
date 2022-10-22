using Agava.YandexGames;
using UnityEngine;

public class Rewarded : MonoBehaviour
{
    [SerializeField] private CoinCollector _player;
    [SerializeField] private Menu _menu;

    private int _modulator;

    public void ShowRewarded(int mululpy = 1)
    {
        _modulator = mululpy;
#if YANDEX_GAMES && !UNITY_EDITOR
        VideoAd.Show(onOpenCallback: Mute, onCloseCallback: UnMute, onRewardedCallback: Reward);
#endif
#if UNITY_EDITOR
        Reward();
#endif
    }

    private void Reward()
    {
        UnMute();
        _player.MultyplyCoins(_modulator);
        _menu.NextLevel();
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