using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CoinCollector coinCollector))
        {
            coinCollector.AddCoin();
            Destroy(gameObject);
        }
    }
}