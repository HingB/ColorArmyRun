using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WallHealthPresenter : MonoBehaviour
{
    [SerializeField] private Wall _wall;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _duration;
    [SerializeField] private GameResults _gameResults;

    private void OnWallHealthChanged(float value, float maxValue)
    {
        _slider.DOValue((value / maxValue) * 100, _duration);
    }

    private void OnEnded()
    {
        _slider.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _wall.HealthChanged += OnWallHealthChanged;
        _gameResults.OnEnded += OnEnded;
    }

    private void OnDisable()
    {
        _wall.HealthChanged -= OnWallHealthChanged;
        _gameResults.OnEnded -= OnEnded;
    }
}