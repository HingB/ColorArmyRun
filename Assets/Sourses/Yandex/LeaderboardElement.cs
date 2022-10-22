using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerNick;
    [SerializeField] private TMP_Text _playerResult;
    [SerializeField] private Image _playerIcon;
    [SerializeField] private Color _playerColor;
    [SerializeField] private List<Sprite> _icons;

    public void Construct(string nick, int playerResult)
    {
        _playerNick.text = nick;
        _playerResult.text = playerResult.ToString();
        _playerIcon.sprite = GetRandomSprite();
    }

    private Sprite GetRandomSprite()
    {
        int spriteIndex = Random.Range(0, _icons.Count);

        return _icons[spriteIndex];
    }
}