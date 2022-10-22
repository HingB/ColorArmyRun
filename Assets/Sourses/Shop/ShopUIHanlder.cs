using UnityEngine;

public class ShopUIHanlder : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private UIButtonsCotroller _buttons;

    private void OnEnable()
    {
        _buttons.ShopButtonClick += OnShopButtonClick;
    }

    private void OnDisable()
    {
        _buttons.ShopButtonClick -= OnShopButtonClick;
    }

    private void OnShopButtonClick()
    {
        _shop.gameObject.SetActive(true);
    }
}