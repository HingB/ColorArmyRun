using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class Localization : MonoBehaviour
{
    [SerializeField] private Lean.Localization.LeanLocalization _localization;

    public static Lean.Localization.LeanLocalization Instance { get; private set; }

    public void FindCurrentLanguage()
    {
#if UNITY_WEBGL && !UNITY_EDITOR && YANDEX_GAMES
        switch (YandexGamesSdk.Environment.i18n.lang)
        {
            case "en":
                _localization.SetCurrentLanguage("English");
                break;
            case "ru":
            case "be":
            case "kk":
            case "uk":
            case "uz":
                _localization.SetCurrentLanguage("Russian");
                break;
            case "tr":
                _localization.SetCurrentLanguage("Turkish");
                break;
            default:
                _localization.SetCurrentLanguage("English");
                break;
        }
#endif
    }

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR && YANDEX_GAMES
        YandexGamesSdk.Initialize(FindCurrentLanguage);
#endif

#if VK_GAMES
        _localization.SetCurrentLanguage("Russian");
#endif
        Instance = _localization;
    }
}
