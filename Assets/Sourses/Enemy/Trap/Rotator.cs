using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _timeRotation = 6;

    private TweenerCore<Quaternion, Vector3, QuaternionOptions> _rotations;

    private void Start()
    {
        var startRotation = transform.eulerAngles;
        _rotations = transform.DORotate(new Vector3(startRotation.x, 359, startRotation.z), _timeRotation, RotateMode.FastBeyond360)
             .SetLoops(-1, LoopType.Restart)
             .SetEase(Ease.Linear);
    }

    private void OnDestroy()
    {
        _rotations.Kill();
    }
}
