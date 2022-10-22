using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButtonsCotroller : MonoBehaviour
{
    [SerializeField] private Button _mute;
    [SerializeField] private Button _shop;
    [SerializeField] private Button _leaders;

    public event UnityAction MuteButtonClick;
    public event UnityAction ShopButtonClick;
    public event UnityAction LeadersButtonClick;

    private void OnEnable()
    {
        _mute?.onClick.AddListener(OnMuteButtonClick);
        _shop?.onClick.AddListener(OnShopButtonClick);
        _leaders?.onClick.AddListener(OnLeadersButtonClick);
    }

    private void OnDisable()
    {
        _mute.onClick?.RemoveListener(OnMuteButtonClick);
        _shop.onClick?.RemoveListener(OnShopButtonClick);
        _leaders.onClick?.RemoveListener(OnLeadersButtonClick);
    }

    private void OnMuteButtonClick()
    {
        MuteButtonClick?.Invoke();
    }

    private void OnShopButtonClick()
    {
        ShopButtonClick?.Invoke();
    }

    private void OnLeadersButtonClick()
    {
        LeadersButtonClick?.Invoke();
    }
}
