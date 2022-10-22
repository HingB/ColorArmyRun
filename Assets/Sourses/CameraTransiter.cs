using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraTransiter : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> _camers;
    [field :SerializeField] public CinemachineVirtualCamera CurrentCamera { get; private set; }

    public void Transit(int number)
    {
        if (number >= _camers.Count || number < 0)
            Debug.LogAssertion("Одна ошибка и ты ошибся");
        _camers[number].Priority = 10;
        CurrentCamera.Priority = 1;
        CurrentCamera = _camers[number];
    }

    public void Transit(CinemachineVirtualCamera camera)
    {
        for (int i = 0; i < _camers.Count; i++)
        {
            if (_camers[i] == camera)
            {
                Transit(i);
                break;
            }
        }
    }

    private void Start()
    {
        foreach (var camera in _camers)
            camera.Priority = 1;
        CurrentCamera.Priority = 10;
    }
}
