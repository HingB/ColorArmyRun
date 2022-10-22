using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class BrushTrigger : Trigger
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Material _currentHandleMaterial;
    private CaseMaterial _caseMaterial;

    private void OnEnable()
    {
        Player.MaterialChanged += ChangeMaterial;
    }

    private void ChangeMaterial(CaseMaterial value)
    {
        _caseMaterial = value;
    }

    private void OnDisable()
    {
        Player.MaterialChanged -= ChangeMaterial;
    }

    private void Awake()
    {
        _currentHandleMaterial = _skinnedMeshRenderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BrushCase brushCase))
        {
            if (_currentHandleMaterial.color == _caseMaterial.BrushHandle.color)
                brushCase.AddBrush();
            else
                brushCase.RemoveBrush();
            Destroy(this.gameObject);
        }
    }
}
