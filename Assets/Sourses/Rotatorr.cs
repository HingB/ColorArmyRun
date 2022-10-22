using System.Collections.Generic;
using UnityEngine;

public class Rotatorr : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _resetSpeed;
    [SerializeField] private float _maxAngle;
    private Quaternion _forwardQuaternionRotation = new Quaternion(0, 0, 0, 1);
    private float _xRotation;
    private float _threshold = 0.001f;
    private float _smoothMultiplier = 10;
    public List<Transform> RotateItem { get; private set; }

    private void OnEnable()
    {
        Bones.BonesAdded += AddBones;
        Bones.BonesRemoved += RemoveBones;
    }

    private void OnDisable()
    {
        Bones.BonesAdded -= AddBones;
        Bones.BonesRemoved -= RemoveBones;
    }

    private void RemoveBones(Transform value)
    {
        RotateItem.Remove(value);
    }

    private void AddBones(Transform value)
    {
        RotateItem.Add(value);
    }

    private void Awake()
    {
        RotateItem = new List<Transform>();
    }

    private void Update()
    {
        float pointerX = Input.GetAxis("Mouse X") * _rotationSpeed * _smoothMultiplier * Time.deltaTime;

        _xRotation += pointerX;

        _xRotation = Mathf.Clamp(_xRotation, -_maxAngle, _maxAngle);

        if (Input.GetMouseButton(0))
        {
            if (Mathf.Abs(pointerX) >= _threshold)
            {
                foreach (var item in RotateItem)
                    item.localRotation = Quaternion.Euler(0, _xRotation, 0f);
            }
        }

        ResetRotation();
    }

    private void ResetRotation()
    {
        foreach (var item in RotateItem)
            item.localRotation = Quaternion.Slerp(item.localRotation, _forwardQuaternionRotation, _resetSpeed * Time.deltaTime);
        if (RotateItem.Count != 0)
            _xRotation = RotateItem[0].localEulerAngles.y;
        if (_xRotation > 180)
            _xRotation -= 360;
    }
}
