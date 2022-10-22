using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthDisplay : MonoBehaviour
{
    [SerializeField, GameObjectOfType(typeof(IHaveHealth))] private GameObject _healthOwner;
    private Slider _slider;
    private IHaveHealth Health => _healthOwner.GetComponent<IHaveHealth>();

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        Health.HealthChanged += ChangeSliderValue;
    }

    private void OnDisable()
    {
        Health.HealthChanged -= ChangeSliderValue;
    }

    private void ChangeSliderValue(float slider)
    {
        _slider.value = Health.Health / Health.MaxHelth;
    }
}
