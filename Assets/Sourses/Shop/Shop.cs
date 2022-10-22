using System;
using UnityEngine;
using System.Linq;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopObject[] _goods;
    [SerializeField] private CoinCollector _moneyHolder;
    [SerializeField] private TMP_Text _infoText;
    [SerializeField] private GameObject _notyfication;

    public int GetAbleToBuyGoodsCount()
    {
        return _goods.Where(good => _moneyHolder.Money >= good.Good.Price).Count();
    }

    public void ShowItemsToBuyInfo(int count)
    {
        _notyfication.SetActive(true);
        _infoText.text = count.ToString();
    }

    public bool TryBuy(Good good)
    {
        if (_moneyHolder.Money >= good.Price)
        {
            _moneyHolder.RemoveCoins(good.Price);
            good.Buy();
            return true;
        }

        return false;
    }

    public void UnSelectAll()
    {
        foreach (var good in _goods)
        {
            good.Good.UnSelect();
        }
    }

    private void OnEnable()
    {
        _notyfication.SetActive(false);
    }

    private void Awake()
    {
        foreach (var good in _goods)
        {
            good.Init(this);
        }

        gameObject.SetActive(false);
    }
}