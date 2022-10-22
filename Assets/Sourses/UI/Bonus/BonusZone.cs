using UnityEngine;

[System.Serializable]
public struct BonusZone
{
    [field: SerializeField] public float StartAngle { get; private set; }
    [field: SerializeField] public float FinishAngle { get; private set; }
    [field: SerializeField] public Multylier Multylier { get; private set; }
    [field: SerializeField] public float Bonus { get; private set; }
}
