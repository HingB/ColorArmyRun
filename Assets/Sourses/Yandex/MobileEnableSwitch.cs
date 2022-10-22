using UnityEngine;

public class MobileEnableSwitch : MonoBehaviour
{
#if VK_GAMES
    private void OnEnable()
    {
        gameObject.SetActive(Application.isMobilePlatform);
    }
#endif
}