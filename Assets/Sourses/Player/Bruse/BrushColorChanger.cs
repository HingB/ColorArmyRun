using PaintIn3D;
using UnityEngine;

public class BrushColorChanger : MonoBehaviour, IPaintable
{
    [SerializeField] private SkinnedMeshRenderer _brushRenderer;
    [SerializeField] private TrailRenderer _trailRenderPrefab;
    [SerializeField] private Transform _newTrailRenderParent;
    [SerializeField] bool IsChangeDistant = false;

    public CaseMaterial CaseMaterial { get; private set; }
    [field :SerializeField] public TrailRenderer CurrentTrailRenderer { get; private set; }

    private void OnEnable()
    {
        Player.MaterialChanged += ChangeCaseMaterial;
    }

    private void OnDisable()
    {
        Player.MaterialChanged -= ChangeCaseMaterial;
    }

    public void ChangeCaseMaterial(CaseMaterial caseMaterial)
    {
        CaseMaterial = caseMaterial;
        if (IsChangeDistant)
            ChangeColor();
    }

    public void ChangeColor()
    {
        DisconnectTrailRender();
        CurrentTrailRenderer = CreateTrailRender();
        _brushRenderer.material = CaseMaterial.BrushHandle;
        ChangeTrailRenderColor(CurrentTrailRenderer);
    }

    public void ChangeColorSimple()
    {
        ChangeTrailRenderColor(CurrentTrailRenderer);
        _brushRenderer.material = CaseMaterial.BrushHandle;
    }

    public void DisconnectTrailRender() => CurrentTrailRenderer.transform.parent = transform.parent.parent;

    private void ChangeTrailRenderColor(TrailRenderer trailRenderer)
    {
        var startColor = trailRenderer.startColor;
        var endColor = trailRenderer.endColor;
        var color = CaseMaterial.Color;
        trailRenderer.startColor = new Color(color.r, color.g, color.b, startColor.a);
        trailRenderer.endColor = new Color(color.r, color.g, color.b, endColor.a);
    }

    private TrailRenderer CreateTrailRender()
    {
        return Instantiate(_trailRenderPrefab, _newTrailRenderParent.position, Quaternion.identity, _newTrailRenderParent);
    }
}