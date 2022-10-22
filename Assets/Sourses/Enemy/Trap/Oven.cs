using System.Collections;
using UnityEngine;

public class Oven : MonoBehaviour
{
    [SerializeField] private float _tick;
    [SerializeField] private float _damageOnTick;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Friend stickman))
        {
            if (stickman is IFryable fryable)
            {
                stickman.Engen.UnderFire = true;
                StartCoroutine(StartFire(stickman, fryable));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Friend stickman))
        {
            if (stickman is IFryable fryable)
            {
                stickman.Engen.UnderFire = false;
                StopCoroutine(StartFire(stickman, fryable));
                stickman.ReturnHealth();
                stickman.LeaveFireAcion();
            }
        }
    }

    private IEnumerator StartFire(Stickman stickman, IFryable fryable)
    {
        Friend friend = (Friend)stickman;
        float health = 100;

        friend.InstantFireMaterial();
        while (stickman.Engen.UnderFire)
        {
            yield return new WaitForSeconds(_tick);

            if (fryable.GiveHeat(_damageOnTick, out health))
                fryable.Fry();

            friend.SetDissolveAmount(health, 100);
        }
    }
}