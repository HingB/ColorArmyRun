using UnityEngine;

public class Item
{
    private readonly Transform _transform;
    private float _radius;
    public Item(Transform transform, float radius)
    {
        _transform = transform;
        _radius = radius;
    }

    public Transform Transform => _transform;

    public Vector3 Position
    {
        get => _transform.position;
        set => _transform.position = value;
    }

    public float Radius => _radius;

    public void UpdateRadius(float radius)
    {
        _radius = radius;
    }
}