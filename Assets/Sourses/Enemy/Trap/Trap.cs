using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Died(other);
        if (other.TryGetComponent(out Brush brush))
        {
            brush.Die();
        }
    }

    private void Died(Collider other)
    {
        if (other.TryGetComponent(out Friend friend))
        {
            var a = friend.transform.position;
            friend.Die();
            friend.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
