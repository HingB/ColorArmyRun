using UnityEngine;
using UnityEngine.Events;

public class EnemyBoss : Enemy, IHaveHealth, IDamageable
{
    [Range(0, 1000)]
    [SerializeField] private float _currentHelth;
    [Range(0, 1000)]
    [SerializeField] private float _maxHealth;

    public event UnityAction<float> HealthChanged;
    public event UnityAction Died;
    public float Health { get; private set; }
    public float MaxHelth => _maxHealth;
    public bool IsDied { get; private set; }

    private void Start()
    {
        Health = _currentHelth;
        HealthChanged?.Invoke(Health);
    }

    private void OnValidate()
    {
        Health = _currentHelth;
        if (Health > _maxHealth)
            _currentHelth = _maxHealth;
        HealthChanged?.Invoke(Health);
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            Debug.LogAssertion("Ты что дурачёк, как можно наносить отрицательный урон");
        Health -= damage;

        Animator.Hit();
        CinemachineScake.Instance?.ShakeCamera(1, .1f);
        if (Health <= 0)
        {
            Health = 0;
            Died?.Invoke();
            IsDied = true;
            this.Die();
            GameSoundsPlayer.Instance?.PlaySound(Sound.BossDie);
        }

        HealthChanged?.Invoke(Health);
    }
}

public interface IHaveHealth
{
    event UnityAction<float> HealthChanged;
    float MaxHelth { get; }
    float Health { get; }
}

public interface IDamageable
{
    event UnityAction Died;
    void TakeDamage(float damage);
}
