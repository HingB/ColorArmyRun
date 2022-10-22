using UnityEngine;

public class DropBullet : Drop
{
    [SerializeField] private float _paintPercent;

    public override void OnStickmanTouch(Stickman stickman)
    {
        if (stickman is IRepaintable repaintable)
        {
            if (repaintable.Paint(_paintPercent))
            {
                if (stickman is Enemy enemy)
                    enemy.BecomeFriend();
            }
        }
    }
}