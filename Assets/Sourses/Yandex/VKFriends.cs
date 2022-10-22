using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VKFriends : MonoBehaviour
{
    private Button _button;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Show);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Show);
    }

    private void Show()
    {
        Agava.VKGames.SocialInteraction.InviteFriends();
    }
}
