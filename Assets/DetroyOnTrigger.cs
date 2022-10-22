using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetroyOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player _) || other.TryGetComponent(out Brush _))
        {
            Destroy(this.gameObject);
        }
    }
}
