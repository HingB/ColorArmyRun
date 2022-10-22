using UnityEngine;

public class AbleToBuyNotify : MonoBehaviour
{
    [SerializeField] private Shop _shop;

    private void Start()
    {
        int count = _shop.GetAbleToBuyGoodsCount();

        if (count > 0)
            _shop.ShowItemsToBuyInfo(count);
    }
}