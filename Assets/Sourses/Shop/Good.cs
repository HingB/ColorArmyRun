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
    [SerializeField] private bool _selected;

    private Shop _shop;

    public int Price => _price;
    public bool Bought => _bought;
    public bool Selected => _selected;
    public Sprite Image => _image;
    public GameObject Self => _self;

    public void Buy()
    {
        _bought = true;
        PlayerPrefs.SetInt("Good" + _number, 1);
    }

    public void Select()
    {
        _selected = true;
        PlayerPrefs.SetInt("Select" + _number, 1);
    }

    public void UnSelect()
    {
        _selected = false;
        PlayerPrefs.SetInt("Select" + _number, 0);
    }

    public void Init(Shop shop)
    {
        _shop = shop;
        _bought = PlayerPrefs.GetInt("Good" + _number) == 1;
        _selected = PlayerPrefs.GetInt("Select" + _number) == 1;
    }
}
