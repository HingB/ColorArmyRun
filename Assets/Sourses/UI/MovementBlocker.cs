using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementBlocker : MonoBehaviour
{
    [SerializeField] private RunnerMovementSystem.Examples.GameMouseInput _mouseInput; 

    private void Update()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);

        if (Input.GetMouseButtonDown(0))
        {
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0)
                BlockMovement();
            else
                UnLockMovement();
        }
    }

    private void BlockMovement()
    {
        _mouseInput.Lock();
    }

    private void UnLockMovement()
    {
        _mouseInput.UnLock();
    }
}