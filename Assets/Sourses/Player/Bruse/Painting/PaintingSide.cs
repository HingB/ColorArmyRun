using DG.Tweening;
using UnityEngine;

public class PaintingSide
{
    private Brush _brush;
    private Transform _startPosition;
    private Rigidbody _brushRigidbody;
    public float Speed { get; set; }

    public void Init(Brush brush, Transform startPosition)
    {
        Speed = 1f;
        _brush = brush;
        _brushRigidbody = _brush.GetComponent<Rigidbody>();
        _startPosition = startPosition;
    }

    public void MoveToStartPosition(float duration)
    {
        _brush.transform.DOMove(_startPosition.position, duration);
        _brush.transform.DORotate(new Vector3(0, -90, 90), duration);
    }

    public void SetDirection(Vector3 direction)
    {
        Paint(direction);
    }

    private void Paint(Vector3 direction)
    {
        Vector3 offset = direction * (Speed * Time.fixedDeltaTime);
        _brushRigidbody.MovePosition(_brushRigidbody.position + offset);
        if (_brushRigidbody.velocity == Vector3.zero)
            return;
        var rotation = Quaternion.LookRotation(_brushRigidbody.velocity, Vector3.back);
        _brushRigidbody.MoveRotation(rotation);
    }
}