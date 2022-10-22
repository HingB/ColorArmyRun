using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopObject : MonoBehaviour
{
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _buy;
    [SerializeField] private Image _photo;

    private Good _good;
    private Shop _shop;
    public Good Good => _good;

    public void Init(Good good, Shop shop)
    {
        _good = good;
        _shop = shop;

        _price.text = good.Price.ToString();
        _photo.sprite = good.Image;
        good.Init(shop);
    }
}