using Agava.WebUtility;
using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class WebGLMuter : MonoBehaviour
{
    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeEvent;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeEvent;
    }

    private void OnInBackgroundChangeEvent(bool enable)
    {
        AudioListener.volume = enable ? 0 : 1;
    }
}
