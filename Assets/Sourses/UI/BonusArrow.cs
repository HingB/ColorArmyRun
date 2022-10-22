using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animation))]
public class BonusArrow : MonoBehaviour
{
    [SerializeField] private Rewarded _rewarded;
    [SerializeField] private BonusZone[] _bonuses;

    private bool _stopped;
    private Animation _animatiion;
    private BonusZone _zone;

    public event UnityAction<BonusZone> BonusZoneChanged;

    public void Stop()
    {
        _animatiion.Stop();
        _stopped = true;

        _rewarded.ShowRewarded((int)_zone.Bonus);
    }

    private void Update()
    {
        if (_stopped == false)
        {
            BonusZone zone = GetCurrentZone();
            if (_zone.Multylier != zone.Multylier)
            {
                _zone = zone;
                BonusZoneChanged?.Invoke(zone);
            }
        }
    }

    private BonusZone GetCurrentZone()
    {
        float rotation = transform.localEulerAngles.z;

        if (rotation > 80)
            rotation -= 360;
        foreach (var zone in _bonuses)
        {
            if (rotation <= zone.StartAngle && rotation >= zone.FinishAngle)
                return zone;
        }

        return _bonuses.First();
    }

    private void Start()
    {
        _animatiion = GetComponent<Animation>();
    }
}