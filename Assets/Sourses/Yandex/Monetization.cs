using System.Collections;
using Agava.YandexGames;
using UnityEngine;

public class Monetization : MonoBehaviour
{
    public IEnumerator ShowInterstitialAfterWhile(float time)
    {
        yield return new WaitForSeconds(time);
        ShowIntersitial();
    }

    public void ShowIntersitial()
    {
#if YANDEX_GAMES
        Agava.YandexGames.InterstitialAd.Show(onOpenCallback: Mute, onCloseCallback: UnMute);
#endif
    }

    private void Mute()
    {
        AudioListener.volume = 0;
    }

    private void UnMute(bool close)
    {
        if (Muter.Instance.Muted == false)
            AudioListener.volume = 1;
    }
}
