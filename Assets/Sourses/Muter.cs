using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MuteSpriteChanger))]
public class Muter : MonoBehaviour
{
    [SerializeField] private UIButtonsCotroller _buttons;

    private bool _muted;

    public event UnityAction<bool> MuteButtonClick;
    public static Muter Instance { get; private set; }

    public bool Muted => _muted;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        _buttons.MuteButtonClick += OnMuteButtonClick;
    }

    private void OnDisable()
    {
        _buttons.MuteButtonClick -= OnMuteButtonClick;
    }

    private void OnMuteButtonClick()
    {
        _muted = !_muted;
        AudioListener.volume = _muted ? 0 : 1;
        MuteButtonClick?.Invoke(_muted);
    }
}