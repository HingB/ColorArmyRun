using System.Collections;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    [SerializeField] private GameResults _gameResults;
    [SerializeField] private Vector3 _shootDirection;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private Transform _parent;
    [SerializeField] private Drop _drop;
    [SerializeField] private Animator _animator;

    public void StartShooting()
    {
        StartCoroutine(StartBurst());
    }

    private void Start()
    {
        _animator.SetFloat("ShootSpeed", _delay);
    }

    private void Shot()
    {
        Instantiate(_drop, _shotPoint.position, transform.localRotation, _parent).Init(Vector3.back, _parent);
        GameSoundsPlayer.Instance?.PlaySound(Sound.Bulk);
    }

    private IEnumerator StartBurst()
    {
        while (_gameResults.Ended == false)
        {
            _animator.SetTrigger("Shoot");
            yield return new WaitForSeconds(_delay);
            Shot();
        }
    }
}