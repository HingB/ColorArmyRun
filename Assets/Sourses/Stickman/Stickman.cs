using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class Stickman : MonoBehaviour, IAttacking, IDyeing, IFryable, IRepaintable
{
    [SerializeField] private float _attackDirection = 1;
    [SerializeField] private float _marginFidelity = 100;
    [SerializeField] protected Animator _animator;
    private Transform _target;
    public Transform Transform => this.transform;
    private Coroutine _goToAttack;

    public event UnityAction<Stickman> Died;
    public event UnityAction<Stickman> TargetDestroyed;
    public event UnityAction ColorChanged;

    public EnemyPool Pool { get; private set; }
    public StickmanEngine Engen { get; private set; }
    public StickmanAnimator Animator { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }

    public void Init(Transform target)
    {
        _target = target;
    }

    public void Init(Transform target, EnemyPool pool)
    {
        _target = target;
        Pool = pool;
    }

    public void Joi()
    {
        Engen.Stop();
        Animator.Joi();
    }

    public void LookAt()
    {
        Engen.LookAt(_target);
    }

    public virtual void Pursue()
    {
        Animator.Move();
        Engen.Pursue(_target);
    }

    public virtual void Attack()
    {
        Animator.Attack();
        Engen.Attack(_target);
    }

    public void Die()
    {
        Animator.Die();
        Engen.Die();
        Died?.Invoke(this);
    }

    public void Stop()
    {
        Engen.Stop();
        Animator.Stay();
    }

    public void Move(Vector3 position)
    {
        Animator.Move();
        Engen.Move(position);
    }

    public Coroutine GoToAttack() => StartCoroutine(GoToAttackIEnumerator());
    public Coroutine GoToAttack(AnimationCurve curve) => _goToAttack = StartCoroutine(GoToAttackIEnumerator(curve));

    public void StopGoToAttack() => StopCoroutine(_goToAttack);
    public IEnumerator GoToAttackIEnumerator()
    {
        var wait = new WaitForFixedUpdate();
        LookAt();
        Pursue();
        while (_target != null)
        {
            if (Engen.GetDistant(_target) <= _attackDirection)
            {
                Stop();
                Attack();
                if (this is EnemyBoss)
                {
                    yield return new WaitForSeconds(1.5f);
                }
                else
                {
                    Die();
                    break;
                }
            }

            yield return wait;
        }

        if (_target != null)
        {
            if (_target.TryGetComponent(out EnemyBoss boss))
                TargetDestroyed?.Invoke(boss);
            else if (_target.TryGetComponent(out Wall wall) == false)
                TargetDestroyed?.Invoke(this);
        }
        else
        {
            TargetDestroyed?.Invoke(this);
        }
    }

    public IEnumerator GoToAttackIEnumerator(AnimationCurve curve)
    {
        var wait = new WaitForFixedUpdate();
        LookAt();
        Pursue();
        var startSpeed = NavMeshAgent.speed;
        while (_target != null)
        {
            var distant = Engen.GetDistant(_target);
            NavMeshAgent.speed = startSpeed * curve.Evaluate(_attackDirection / distant);
            if (distant <= _attackDirection)
            {
                Stop();
                Attack();
                if (this is EnemyBoss boss)
                {
                    yield return new WaitForSeconds(1.5f);
                }
                else
                {
                    Die();
                    break;
                }
            }

            yield return wait;
        }

        TargetDestroyed?.Invoke(this);
    }

    public void Fry()
    {
        Engen.Die();
        Died?.Invoke(this);
    }

    public bool GiveHeat(float damage, out float health)
    {
        return Engen.TakeHeat(damage, out health);
    }

    public void ReturnHealth()
    {
        Engen.ReturnHealth();
    }

    public bool Paint(float persent)
    {
        ColorChanged?.Invoke();
        Animator.Mark();
        return Engen.Repaint(persent);
    }

    protected void InitStickman(StickmanEngine engine, StickmanAnimator animator)
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Engen = engine;
        Engen.InitEngine(this, NavMeshAgent, _marginFidelity);
        Animator = animator;
        Animator.InitAnimator(_animator);
    }
}