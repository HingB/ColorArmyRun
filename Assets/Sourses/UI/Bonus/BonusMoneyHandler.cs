using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusMoneyHandler : MonoBehaviour
{
    [SerializeField] private BonusArrow _arrow;
    [SerializeField] private TMP_Text _bonusText;
    [SerializeField] private TMP_Text _nextLevelText;

    private float _money;

    public void OnFinish()
    {
        _money = FindObjectOfType<CoinCollector>().CollectedMoney;
        _nextLevelText.text = _money.ToString();
        _bonusText.text = _money.ToString();
    }

    private void OnBonusZoneChanged(BonusZone zone)
    {
        _bonusText.text = (_money * zone.Bonus).ToString();
    }

    private void OnEnable()
    {
        _arrow.BonusZoneChanged += OnBonusZoneChanged;
        OnFinish();
    }

    private void OnDisable()
    {
        _arrow.BonusZoneChanged -= OnBonusZoneChanged;
    }
}