using System.Collections;
using UnityEngine;

public class FovCamera : MonoBehaviour
{
    [SerializeField] private float _minFov;
    [SerializeField] private float _maxFov;
    [SerializeField] private float _duration = 1;
    [SerializeField] private RunnerMovementSystem.Examples.GameMouseInput _mouseInput;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera[] _virtualCameras;

    private float _standardFOV = 60;
    private bool _roadEnd = false;

    private void Start()
    {
        _standardFOV = Camera.main.fieldOfView;
    }

    private void OnEnable()
    {
        _mouseInput.IsStopMoved += OnStopMoved;
        _mouseInput.IsContinueMoved += OnStartMoved;
    }

    private void OnDisable()
    {
        _mouseInput.IsStopMoved -= OnStopMoved;
        _mouseInput.IsContinueMoved -= OnStartMoved;
    }

    public void OnFinishEnter()
    {
        _roadEnd = true;
        OnStopMoved();
    }

    private void OnStartMoved()
    {
        MaximizeFov();
    }

    private void OnStopMoved()
    {
        MinimizeFov();
    }

    private void MaximizeFov()
    {
        StopAllCoroutines();
        if (_roadEnd)
            return;
        foreach (var camera in _virtualCameras)
        {
            StartCoroutine(DoFov(camera.m_Lens.FieldOfView, _maxFov, _duration, camera));
        }
    }

    private void MinimizeFov()
    {
        StopAllCoroutines();
        if (_roadEnd)
            return;
        foreach (var camera in _virtualCameras)
        {
            StartCoroutine(DoFov(camera.m_Lens.FieldOfView, _minFov, _duration, camera));
        }
    }

    private void StandatizeFov()
    {
        if (_roadEnd)
            return;
        foreach (var camera in _virtualCameras)
        {
            StartCoroutine(DoFov(camera.m_Lens.FieldOfView, _standardFOV, _duration, camera));
        }
    }

    private IEnumerator DoFov(float current, float endValue, float duration, Cinemachine.CinemachineVirtualCamera camera)
    {
        if (current < endValue)
        {
            while (camera.m_Lens.FieldOfView < endValue)
            {
                camera.m_Lens.FieldOfView += 0.1f;
                yield return new WaitForSeconds(duration / 200);
            }
        }
        else
        {
            while (camera.m_Lens.FieldOfView > endValue)
            {
                camera.m_Lens.FieldOfView -= 0.1f;
                yield return new WaitForSeconds(duration / 200);
            }
        }
    }
}