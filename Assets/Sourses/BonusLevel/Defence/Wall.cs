using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wall : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth;
    private float _health;

    public event UnityAction Died;
    public event UnityAction<float, float> HealthChanged;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        _health -= damage;
        HealthChanged?.Invoke(_health, _maxHealth);

        if (_health <= 0)
        {
            Died?.Invoke();
            _health = 0;
        }
    }
}
