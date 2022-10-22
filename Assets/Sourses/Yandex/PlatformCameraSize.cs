using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCameraSize : MonoBehaviour
{
    [SerializeField] private CameraTransiter _transiter;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _desktop;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _mobile;

    private void Start()
    {
#if YANDEX_GAMES && UNITY_WEBGL
        switch (Agava.YandexGames.Device.Type)
        {
            case Agava.YandexGames.DeviceType.Desktop:
                _transiter.Transit(_desktop);
                break;
            default:
                _transiter.Transit(_mobile);
                break;
        }
#endif

#if VK_GAMES && UNITY_WEBGL
        if (Application.isMobilePlatform)
            _transiter.Transit(_mobile);
        else
            _transiter.Transit(_desktop);
#endif
    }
}
