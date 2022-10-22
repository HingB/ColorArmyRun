using DG.Tweening;
using UnityEngine;

public class EnemyColor : MonoBehaviour
{
    [SerializeField] private ColorStage[] _stages;
    [SerializeField] private Stickman _stickman;
    [SerializeField] private float _paintSpeed;
    [SerializeField] private Material _material;
    [SerializeField] private SkinnedMeshRenderer _render;

    private int _currentStage;

    private void OnRepaint()
    {
        _currentStage++;
        Material material = new Material(_render.material);
        if (_currentStage < _stages.Length)
        {
            material.color = _stages[_currentStage - 1].Color;
            material.DOColor(_stages[_currentStage].Color, 0.4f);
            material.SetColor("_ColorDim", _stages[_currentStage].ColorShaded);
            material.SetColor("_FlatRimColor", _stages[_currentStage].ColorRim);
            _render.material = material;
        }
    }

    private void OnEnable()
    {
        _stickman.ColorChanged += OnRepaint;
    }

    private void OnDisable()
    {
        _stickman.ColorChanged -= OnRepaint;
    }
}

[System.Serializable]
public class ColorStage
{
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public Color ColorShaded { get; private set; }
    [field: SerializeField] public Color ColorRim { get; private set; }
}