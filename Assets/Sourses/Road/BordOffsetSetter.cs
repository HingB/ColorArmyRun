using System;
using System.Collections.Generic;
using RunnerMovementSystem;
using UnityEngine;

public class BordOffsetSetter : MonoBehaviour
{
    [SerializeField] private MovementSystem _movementSystem;
    [SerializeField] private BrushCase _brushCase;
    [SerializeField] private List<SizeCase> _sizeCases;

    private void OnEnable()
    {
        _brushCase.CountChanged+= ChangeOffset;
    }

    private void OnDisable()
    {
        _brushCase.CountChanged -= ChangeOffset;
    }

    private void ChangeOffset(int value)
    {
        _movementSystem.Options.BorderOffset =_sizeCases[(int)value].Offset;
    }
}

[Serializable]
public class SizeCase
{
    [SerializeField] private int _count;
    [field: SerializeField] public float Offset { get; private set; }
}
