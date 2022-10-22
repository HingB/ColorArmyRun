using UnityEngine;
using UnityEngine.Events;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private int _modulator = 3;
    [SerializeField] private int _coins;

    private int _collectedCoins = 0;

    public event UnityAction<int> OnCoinsValueChanged;
    public static CoinCollector Instance { get; private set; }
    public int Money => _coins;

    public void AddCoin()
    {
        _coins++;
        _collectedCoins++;
        Notyfy();
    }

    public void AddCoin(int value)
    {
        if (value < 0)
            return;

        _coins += value;
        _collectedCoins += value;
        Notyfy();
    }

    public void RemoveCoins(int value)
    {
        if (value > 0)
            _coins -= value;

        Notyfy();
    }

    public void AcceptCoins()
    {

    }

    public void MultyplyCoins()
    {
        int coinsToAdd = (_collectedCoins * _modulator) - _collectedCoins;

        AddCoin(coinsToAdd);
    }

    private void Notyfy()
    {
        OnCoinsValueChanged?.Invoke(_coins);
        SaveData();
    }

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        _coins = PlayerPrefs.GetInt("Coins");
        OnCoinsValueChanged?.Invoke(_coins);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("Coins", _coins);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnValidate()
    {
        SaveData();
    }
}