using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopObject : MonoBehaviour
{
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _buy;
    [SerializeField] private Image _photo;
    [SerializeField] private GameObject _lock;
    [SerializeField] private Good _good;

    private Shop _shop;
    public Good Good => _good;

    public void Init(Shop shop)
    {
        _good.Init(shop);
        _shop = shop;

        UpdateText();
        UpdateState();
        _photo.sprite = _good.Image;
    }

    private void OnButtonClick()
    {
        if (_good.Bought)
        {
            _shop.UnSelectAll();
            _good.Select();
        }
        else
        {
            _shop.TryBuy(Good);
        }

        UpdateState();
        UpdateText();
    }

    private void UpdateState()
    {
        if (_good.Bought == false)
        {
            _lock.SetActive(!_shop.CheckAbilityToBuy(Good));
        }
        else
        {
            _lock.SetActive(false);
        }
    }

    private void UpdateText()
    {
        if (_good.Bought)
        {
            if (_good.Selected)
                _price.text = GetTranslitedText("Выбран", "Selected", "Seçme");
            else
                _price.text = GetTranslitedText("Выбрать", "Select", "Seçmek");
        }
        else
        {
            _price.text = _good.Price.ToString();
        }
    }

    private string GetTranslitedText(string ru, string en, string tr)
    {
#if YANDEX_GAMES
        switch (Localization.CurrentLanguage)
        {
            case "English":
                return en;
            case "Russian":
                return ru;
            case "Turkish":
                return tr;
        }
#endif

        return ru;
    }

    private void OnEnable()
    {
        _buy.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _buy.onClick.RemoveListener(OnButtonClick);
    }
}