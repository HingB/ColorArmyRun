using System.Collections.Generic;
using UnityEngine;

internal class ItemMover
{
    private readonly List<Vector3> _positions = new List<Vector3>();
    private readonly Item _targetItem;
    private readonly Item _stalkerItem;
    public ItemMover(Item target, Item stalker)
    {
        _targetItem = target;
        _stalkerItem = stalker;
        _positions.Add(target.Position);
        _positions.Add(stalker.Position);
    }

    public void Move()
    {
        float requiredDistance = _targetItem.Radius + _stalkerItem.Radius;
        float currentDistance = (_targetItem.Position - _positions[0]).magnitude;
        if (currentDistance > requiredDistance)
        {
            Vector3 direction = (_targetItem.Position - _positions[0]) / currentDistance;

            _positions.Insert(0, _positions[0] + (direction * requiredDistance));
            _positions.RemoveAt(_positions.Count - 1);

            currentDistance -= requiredDistance;
        }

        _stalkerItem.Position = Vector3.Lerp(_positions[1], _positions[0], currentDistance / requiredDistance);
    }
}