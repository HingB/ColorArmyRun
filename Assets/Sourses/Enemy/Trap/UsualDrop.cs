using System.Collections;
using UnityEngine;
using System.Linq;

public class UsualDrop : Drop
{
    public override void OnStickmanTouch(Stickman stickman)
    {
        stickman.Die();
        if (stickman.TryGetComponent(out BoxCollider collider))
            collider.enabled = false;
    }
}