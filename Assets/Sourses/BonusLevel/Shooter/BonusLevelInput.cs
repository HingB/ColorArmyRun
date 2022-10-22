using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BonusLevelInput : MouseInput
{
    [SerializeField] private Image _inputZone;
    [SerializeField] private WaterGunLafet _waterGun;
    [SerializeField] private Camera _camera;
    [SerializeField] private bool _mobile;
    [SerializeField] private float _mobileModifier = 1.1f;

    private Vector3 _mousePosition;

    public event UnityAction<Vector3> MousePostionChanged;

    public override void OnMouseButton() => LookAt();

    public override void OnMouseButtonDown() => LookAt();

    public override void OnMouseButtonUp()
    {

    }

    private void FixedUpdate()
    {
        if (Input.mousePosition == _mousePosition)
            return;
        LookAt();
    }

    private void LookAt()
    {
        var offset = Input.mousePosition - _mousePosition;

        if (Physics.Raycast(_camera.ScreenPointToRay(_mousePosition), out var hit, Mathf.Infinity))
            _waterGun.LookAt(hit.point);

        MousePostionChanged?.Invoke(_mousePosition);
    }
}