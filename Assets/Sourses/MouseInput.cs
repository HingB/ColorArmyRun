using UnityEngine;

public abstract class MouseInput : MonoBehaviour
{
    public abstract void OnMouseButtonDown();
    public abstract void OnMouseButtonUp();
    public abstract void OnMouseButton();

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
            OnMouseButtonDown();
        if (Input.GetMouseButton(0))
            OnMouseButton();
        if (Input.GetMouseButtonUp(0))
            OnMouseButtonUp();
    }
}
