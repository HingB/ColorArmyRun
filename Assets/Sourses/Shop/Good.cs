using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Good")]
public class Good : ScriptableObject
{
    [SerializeField] private int _price;
    [SerializeField] private int _number;
    [SerializeField] private Sprite _image;
    [SerializeField] private GameObject _self;
    [SerializeField] private bool _bought;

    private Shop _shop;

    public int Price => _price;
    public bool Bought => _bought;
    public Sprite Image => _image;
    public GameObject Self => _self;

    public void Buy()
    {
        _bought = true;
        PlayerPrefs.SetInt("Good" + _number, 1);
    }

    public void Init(Shop shop)
    {
        _shop = shop;
        if (PlayerPrefs.GetInt("Good" + _number) == 1)
            _bought = true;
        else
            _bought = false;
    }
}
