using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private ShopObject _template;
    [SerializeField] private CoinCollector _moneyHolder;

    internal int GetAbleToBuyGoodsCount()
    {
        throw new NotImplementedException();
    }

    internal void ShowItemsToBuyInfo(int count)
    {
        throw new NotImplementedException();
    }

    [SerializeField] private Good[] _goods;

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

    private void Start()
    {
        foreach (var good in _goods)
        {
            ShopObject shopObject = Instantiate(_template, _container);

            shopObject.Init(good, this);
        }
    }
}