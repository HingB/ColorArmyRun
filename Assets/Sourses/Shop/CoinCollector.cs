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
    public int CollectedMoney => _collectedCoins;

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

        _collectedCoins += value;
    }

    public void RemoveCoins(int value)
    {
        if (value > 0)
            _coins -= value;

        Notyfy();
    }

    public void AcceptCoins()
    {
        _coins += _collectedCoins;

        Notyfy();
        YandexLeaderboard.Instance.AddPlayerToLeaderboard(_coins);
    }

    public void MultyplyCoins(int modul)
    {
        int coinsToAdd = (_collectedCoins * modul) - _collectedCoins;
        Debug.Log(modul);

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
        Debug.Log(_coins);
        OnCoinsValueChanged?.Invoke(_coins);
    }

    private void SaveData()
    {
        Debug.Log("Save");
        PlayerPrefs.SetInt("Coins", _coins);
    }

    private void Awake()
    {
        Instance = this;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        //SaveData();
    }
#endif
}