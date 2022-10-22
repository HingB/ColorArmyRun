using UnityEngine;

public class PaitingBrushAnimator : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _hightBrush = 4;
    private Rigidbody _rigidbody;
    private Vector3 _lastPosition;
    private Vector3 _normalizeDirection;
    private float _sqrDistansOverwriting;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = _transform.position - _lastPosition;
        var sqrMagnitude = direction.sqrMagnitude;
        if (sqrMagnitude > _sqrDistansOverwriting)
        {
            _lastPosition = _transform.position;
            _normalizeDirection = direction / Mathf.Sqrt(sqrMagnitude);
        }

        _rigidbody.velocity = -(_normalizeDirection * _speed) + (Vector3.forward * _hightBrush);
    }
}
