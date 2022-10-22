using System.Collections.Generic;
using UnityEngine;

public class CaseItem
{
    public readonly Transform Head;
    private CaseEngine _engine;
    private float _radius;
    public CaseItem(Transform head, float radius)
    {
        Head = head;
        _radius = radius;
        _engine = new CaseEngine();
        _engine.Add(new Item(head, _radius));
    }

    public IReadOnlyList<Item> Items => _engine.CaseObjects;

    public void Update()
    {
        _engine.Update();
    }

    public void UpdateRadius(float radius)
    {
        foreach (var item in _engine.CaseObjects)
            item.UpdateRadius(radius);
        _radius = radius;
    }

    public void Add(Transform transform) => _engine.Add(new Item(transform, _radius));

    public bool TryRemove(Transform transform)
    {
        for (int i = 0; i < _engine.CaseObjects.Count; i++)
        {
            if (_engine.CaseObjects[i].Transform == transform)
            {
                _engine.Remove(i);
                return true;
            }
        }

        return false;
    }

    public bool TryRemove(int number)
    {
        var isRemove = _engine.CaseObjects.Count != 0;
        if (_engine.CaseObjects.Count != 0)
            _engine.Remove(number);
        return isRemove;
    }
}