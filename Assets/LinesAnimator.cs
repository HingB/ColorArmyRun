using System.Collections;
using UnityEngine;

public class LinesAnimator : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private float _speed = 1;
    private Coroutine _animation;

    private void Start()
    {
        StartAnimation();
    }
    public void StartAnimation() => _animation = StartCoroutine(AnimateTexture());
    public void StopAnimation() => StopCoroutine(_animation);

    private IEnumerator AnimateTexture()
    {
        _material.color = Color.white;
        var wait = new WaitForFixedUpdate();
        float time = 0;
        while (true)
        {
            time += Time.fixedDeltaTime * _speed;
            _material.mainTextureOffset = new Vector2(0, time);
            yield return wait;
        }
    }

    public void DisableLine()
    {
        _material.color = new Color(0, 0, 0, 0);
    }
}
