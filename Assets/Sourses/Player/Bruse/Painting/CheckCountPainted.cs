using PaintIn3D;
using UnityEngine;
using UnityEngine.Events;

public class CheckCountPainted : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float _persentColor;
    [Range(0, 100)]
    [SerializeField] private float _persentToWin;
    [SerializeField] private int _countToWinPixelPaint = 100000;
    [SerializeField] private int _currentPixelPaint;
    [SerializeField] private P3dColor _p3DColor;
    [SerializeField] private Transform _fillPaint;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private ParticleSystem[] _winParticlePrefab;

    private P3dColorCounter _p3DColorCounter;

    public event UnityAction FullPaited;

    private void OnEnable()
    {
        _p3DColorCounter = FindObjectOfType<ChekPaint>().GetComponent<P3dColorCounter>();
        _p3DColorCounter.OnUpdated += ChangeCount;
    }

    private void OnDisable()
    {
        _p3DColorCounter.OnUpdated -= ChangeCount;
    }

    private void ChangeCount()
    {
        _currentPixelPaint = _p3DColorCounter.Count(_p3DColor);
        int result = 100 * _currentPixelPaint / _countToWinPixelPaint;
        _persentColor = result;
        if (_persentColor >= _persentToWin)
        {
            ColorCasle();
            FullPaited?.Invoke();
        }
    }

    private void ColorCasle()
    {
        _fillPaint.gameObject.SetActive(true);
        foreach (var item in _winParticlePrefab)
        {
            item.Play();
        }
    }

}
