using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Muter))]
[RequireComponent(typeof(Image))]
public class MuteSpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite _mute;
    [SerializeField] private Sprite _unmute;
    [SerializeField] private Image _image;

    private Muter _muter;

    private void Start()
    {
        _muter = GetComponent<Muter>();
        _muter.MuteButtonClick += OnMuteButtonClick;
    }

    private void OnMuteButtonClick(bool muted)
    {
        _image.sprite = muted ? _mute : _unmute;
    }

    private void OnDisable()
    {
        _muter.MuteButtonClick -= OnMuteButtonClick;
    }
}