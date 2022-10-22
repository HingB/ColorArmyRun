using Cinemachine;
using UnityEngine;

public class CinemachineScake : MonoBehaviour
{
    public static CinemachineScake Instance { get; private set; }
    private CinemachineBasicMultiChannelPerlin _cin;
    private float _shakeTimer;

    private void Awake()
    {
        Instance = this;
        _cin = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        _cin.m_AmplitudeGain = intensity;
        _shakeTimer = time;
    }

    private void Update()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0f)
            {
                _cin.m_AmplitudeGain = 0f;
            }
        }
    }
}
