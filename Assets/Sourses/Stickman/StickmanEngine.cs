using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StickmanEngine
{
    public bool UnderFire;
    public bool IsFriend;

    private MonoBehaviour _monoBehaviour;

    private Transform _transform;

    private NavMeshAgent _navMeshAgent;

    private WaitForSeconds _time;

    private Coroutine _look;
    private Coroutine _pursue;
    private float _healthHeat = 100;
    private float _marginFidelity = 100;

    public void InitEngine(MonoBehaviour stickman, NavMeshAgent navMeshAgent, float marginFidelity)
    {
        _monoBehaviour = stickman;
        _transform = stickman.transform;
        _navMeshAgent = navMeshAgent;
        _marginFidelity = marginFidelity;
        _time = new WaitForSeconds(0.05f);
    }

    public float GetDistant(Transform target) => GetDistant(target.position);
    public float GetDistant(Vector3 target) => (target - _transform.position).magnitude;

    public void Attack(Transform target)
    {
        if (target.TryGetComponent(out IDamageable damagebleTarget))
            damagebleTarget.TakeDamage(100);
    }

    public void Die()
    {
        Stop();
        if (_transform != null)
            Object.Destroy(_transform.gameObject);
    }

    public bool TakeHeat(float value, out float health)
    {
        if (value < 0)
        {
            Debug.Log("Как ты урон меньше 0 наносишь?");
        }

        if (_healthHeat - value <= 0)
        {
            _healthHeat = 0;
            health = 0;
            return true;
        }

        _healthHeat -= value;
        health = _healthHeat;
        return false;
    }

    public bool Repaint(float persent)
    {
        if (persent < 0)
        {
            Debug.Log("Как ты назад красишь?");
            return false;
        }

        _marginFidelity -= persent;
        if (_marginFidelity <= 0)
        {
            _marginFidelity = 0;
            return true;
        }

        return false;
    }

    public void ReturnHealth()
    {
        _healthHeat = 100;
    }

    public void LookAt(Transform target) => RetartCorutine(ref _look, LookAtTarget(target));

    public void Pursue(Transform target) => RetartCorutine(ref _pursue, PursueTarget(target));

    public void Stop()
    {
        if (_pursue != null)
            _monoBehaviour.StopCoroutine(_pursue);
        if (_navMeshAgent != null)
        {
            if (_navMeshAgent.enabled == true)
                _navMeshAgent.destination = _transform.transform.position;
        }
    }

    public void Move(Vector3 target)
    {
        if (_navMeshAgent.isOnNavMesh)
            _navMeshAgent.destination = target;
    }

    private IEnumerator PursueTarget(Transform target)
    {
        while (target != null)
        {
            Move(target.position);
            yield return _time;
        }
    }

    private IEnumerator LookAtTarget(Transform target)
    {
        while (target != null)
        {
            var direction = new Vector3(target.position.x, _transform.position.y, target.position.z);
            _transform.LookAt(direction);
            yield return _time;
        }
    }

    private void RetartCorutine(ref Coroutine current, IEnumerator next)
    {
        if (current != null)
            _monoBehaviour.StopCoroutine(current);
        if (next != null)
            current = _monoBehaviour.StartCoroutine(next);
    }
}