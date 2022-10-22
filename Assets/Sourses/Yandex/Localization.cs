using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class Localization : MonoBehaviour
{
    [SerializeField] private Lean.Localization.LeanLocalization _localization;

    public static Lean.Localization.LeanLocalization Instance { get; private set; }
    public static string CurrentLanguage { get; private set; }


    public void FindCurrentLanguage()
    {
        Debug.Log("InitSDKDone");
        switch (YandexGamesSdk.Environment.i18n.lang)
        {
            case "en":
                _localization.SetCurrentLanguage("English");
                CurrentLanguage = "English";
                break;
            case "ru":
            case "be":
            case "kk":
            case "uk":
            case "uz":
                _localization.SetCurrentLanguage("Russian");
                CurrentLanguage = "Russian";
                break;
            case "tr":
                _localization.SetCurrentLanguage("Turkish");
                CurrentLanguage = "Turkish";
                break;
            default:
                _localization.SetCurrentLanguage("English");
                CurrentLanguage = "English";
                break;
        }
    }

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR && YANDEX_GAMES
        StartCoroutine(InitYandex());
        Debug.Log("InitSDKStart");
#endif

#if VK_GAMES
        _localization.SetCurrentLanguage("Russian");
#endif
        Instance = _localization;
    }

    private IEnumerator InitYandex()
    {
        yield return YandexGamesSdk.Initialize(FindCurrentLanguage);
    }
}
