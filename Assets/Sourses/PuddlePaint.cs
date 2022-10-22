using PaintIn3D;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PuddlePaint : Trigger
{
    [SerializeField] private CaseMaterial _caseMaterial;
    [SerializeField] private P3dPaintDecal _p3DPaintDecal;
    [SerializeField] private P3dColor _p3DColor;
    [SerializeField] private P3dPaintSphere _p3DPaintSphere;

    private bool isTriggerPlayer;
    private List<IPaintable> _paintables;

    private void Start()
    {
        InitTrigger();
        _paintables = new List<IPaintable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) && isTriggerPlayer == false)
        {
            player.ChangeMaterials(_caseMaterial);
            isTriggerPlayer = true;
            _p3DPaintDecal.Color = _caseMaterial.Color;
            _p3DColor.Color = _caseMaterial.Color;
            _p3DPaintSphere.Color = _caseMaterial.Color;
        }

        if (other.TryGetComponent(out IPaintable paintable) && _paintables.Contains(paintable) == false)
        {
            paintable.ChangeColor();
            _paintables.Add(paintable);
        }
    }
}
