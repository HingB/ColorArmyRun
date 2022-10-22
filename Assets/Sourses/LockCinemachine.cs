using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")]
public class LockCinemachine : CinemachineExtension
{
    [Tooltip("Lock the camera's X position to this value")]
    public float _xPosition = 0;
    public float _yPosition = 0;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            var pos = state.RawPosition;
            pos.x = _xPosition;
            pos.y = _yPosition;
            state.RawPosition = pos;
        }
    }
}
