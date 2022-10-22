using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishMoneyMultyplier : MonoBehaviour
{
    [SerializeField] private float _textSpeed;
    [SerializeField] private TMP_Text _collectedMoney;
    [SerializeField] private TMP_Text _usualMoney;
    [SerializeField] private Button _bonusButton;
    [SerializeField] private Button _usualButton;
    [SerializeField] private Rewarded _rewarded;
    [SerializeField] private BonusArrow _arrow;

    private Multylier _current;
    private float _money;

    private void OnFinish(float collectedMoney)
    {
        UpdateText(collectedMoney);
        _money = collectedMoney;
        _usualMoney.text = collectedMoney.ToString();
    }

    private void UpdateText(float money)
    {
        _collectedMoney.text = money.ToString();
    }

    private void OnEnable()
    {
        OnFinish(CoinCollector.Instance.Money);
        _arrow.BonusZoneChanged += OnBonusZoneChanged;
        _usualButton.onClick.AddListener(OnBonusButtonClick);
    }

    private void OnBonusButtonClick()
    {
        CoinCollector.Instance.AcceptCoins();
    }

    private void OnBonusZoneChanged(BonusZone zone)
    {
        UpdateText(_money * zone.Bonus);
    }
}

public enum Multylier
{
    Red,
    Yellow,
    Green
}