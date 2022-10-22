using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class ItemAnimator : MonoBehaviour
{
    [SerializeField] private float _timeRotation = 6;
    [SerializeField] private float _timeScale = 2;

    private TweenerCore<Quaternion, Vector3, QuaternionOptions> _rotations;
    private TweenerCore<Vector3, Vector3, VectorOptions> _scale;

    private void Start()
    {
        var startRotation = transform.eulerAngles;
        _rotations = transform.DORotate(new Vector3(startRotation.x, 359, startRotation.z), _timeRotation, RotateMode.FastBeyond360)
             .SetLoops(-1, LoopType.Restart)
             .SetEase(Ease.Linear);
        _scale = transform.DOScale(1.2f, _timeScale)
             .SetLoops(-1, LoopType.Yoyo)
             .SetEase(Ease.Linear);
    }

    private void OnDestroy()
    {
        _rotations.Kill();
        _scale.Kill();
    }
}
