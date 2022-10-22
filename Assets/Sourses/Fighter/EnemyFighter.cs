using System.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Enemy), typeof(SphereCollider))]
public class EnemyFighter : MonoBehaviour
{
    [SerializeField] private float _speedToMove = 10;
    [SerializeField] private float _distantToTrigger = 40;
    [SerializeField] private float _distantToAttack = 10;
    private Friend _friend;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        GetComponent<SphereCollider>().radius = _distantToTrigger;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Friend friend) && _friend == null && friend.IsTrigger == false && friend.Follow == true)
        {
            _friend = friend;
            _friend.IsTrigger = true;
            _friend.Figth();
            StartCoroutine(Fight());
        }
    }

    private IEnumerator Fight()
    {
        _friend.Init(_enemy.transform);
        _enemy.Init(_friend.transform);
        _friend.NavMeshAgent.speed = _speedToMove;
        _friend.GoToAttack();
        yield return new WaitWhile(() => _friend == null ? false : _distantToAttack < _friend.Engen.GetDistant(this.transform));
        if (_friend != null)
        _enemy.GoToAttack();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _distantToTrigger * this.transform.localScale.x);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _distantToAttack * this.transform.localScale.x);
    }
#endif
}