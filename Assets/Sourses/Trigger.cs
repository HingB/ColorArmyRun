using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class Trigger : MonoBehaviour
{
    protected void InitTrigger()
    {
        var boxcollider = GetComponent<BoxCollider>();
        var rigidbody = GetComponent<Rigidbody>();
        boxcollider.isTrigger = true;
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
    }

    private void Start()
    {
        InitTrigger();
    }
}
