using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderMover : MonoBehaviour
{
    [Range(0,10)]
    [SerializeField] private float _speed;
    private Slider _slider;
    private float _sign = 1;

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        _slider.value += Time.deltaTime * _speed * _sign;
        if (_slider.value == 1 || _slider.value == 0)
            _sign = -_sign;
    }
}
