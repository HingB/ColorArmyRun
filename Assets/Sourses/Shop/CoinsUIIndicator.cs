using TMPro;
using UnityEngine;

public class CoinsUIIndicator : MonoBehaviour
{
    [SerializeField] private CoinCollector _coinCollector;
    [SerializeField] private TMP_Text _coins;

    private void OnEnable()
    {
        _coinCollector.OnCoinsValueChanged += OnCoinsValueChanged;
    }

    private void OnDisable()
    {
        _coinCollector.OnCoinsValueChanged -= OnCoinsValueChanged;
    }

    private void OnCoinsValueChanged(int value)
    {
        _coins.text = value.ToString();
    }
}