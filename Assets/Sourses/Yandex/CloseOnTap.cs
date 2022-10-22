using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CloseOnTap : MonoBehaviour
{
    [SerializeField] private UnityEvent _onClose;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    private void Update()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);

        if (Input.GetMouseButtonDown(0))
        {
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count == 0)
            {
                CloseSelf();
            }
        }
    }

    public void CloseSelf()
    {
        _onClose?.Invoke();
        gameObject.SetActive(false);
    }
}
