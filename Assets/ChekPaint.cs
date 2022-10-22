using PaintIn3D;
using UnityEngine;

[RequireComponent(typeof(P3dColorCounter))]
public class ChekPaint : MonoBehaviour
{
    [SerializeField] private GameObject[] _casleSide;
    [SerializeField] private P3dPaintSphere[] _FillSide;
    [SerializeField] private int _currentPaintSide;
    private SceneLoader _sceneLoader;
    public int CurrentPaintSide => _currentPaintSide;
    public P3dColorCounter P3DColorCurrentSide { get; private set; }
    public int SideCount { get; private set; }
    private void Start()
    {
        SideCount = _casleSide.Length;
        P3DColorCurrentSide = GetComponent<P3dColorCounter>();
        if (_casleSide.Length > (int)CasleSide.Count)
            Debug.LogAssertion("Pleas Add Sides in SceneLoader");
        _sceneLoader = FindObjectOfType<SceneLoader>();
        for (int i = 0; i < _casleSide.Length; i++)
        {
            if (i == _currentPaintSide)
                SetLayer((CasleSide)i, "Casle");
            else
                SetLayer((CasleSide)i, ((CasleSide)i).ToString());
        }

#if UNITY_EDITOR
        if (_sceneLoader == null)
            return;
#endif
        if (_currentPaintSide == 0)
        {
            SceneLoader.Instance.RemoveColor();
        }

        for (int i = 0; i < _casleSide.Length; i++)
        {
            if (_currentPaintSide > i)
            {
                PaintSide((CasleSide)i, _sceneLoader.TakeColor((CasleSide)i));
            }
            else
            {
                P3DColorCurrentSide.PaintableTexture = _casleSide[i].GetComponent<P3dPaintableTexture>();
                break;
            }
        }
    }

    private void PaintSide(CasleSide side, Color color)
    {
        SetLayer(side, side.ToString());
        _FillSide[(int)side].Color = color;
        _FillSide[(int)side].gameObject.SetActive(true);
    }

    private void SetLayer(CasleSide casleSide, string layerName) => _casleSide[(int)casleSide].layer = LayerMask.NameToLayer(layerName);
}
