using Assets.Sourses.Tag;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(Stickman))]
public class BossFighter : MonoBehaviour
{
    [SerializeField] private float _distantToTrigger;
    [SerializeField] private TagLosePanel _losePanel;
    [SerializeField] private AnimationCurve _speedByDistant;
    [Range(0, 50)]
    [SerializeField] private int _maxSpeed = 18;
    [Range(0, 50)]
    [SerializeField] private int _minSpeed = 9;
    private Transform _newPosition;

    private Stickman _boss;
    private List<Friend> _friends;
    private Player _player;
    private PanelOpener _panelOpener;
    private SphereCollider _collider;
    private bool _isFigthTrigger = false;

    public event UnityAction<List<Friend>> FightEnd;

    private void OnEnable()
    {
        _boss = this.GetComponent<Stickman>();
        Friend.FriendDie += RemoveFriend;
        ((IDamageable)_boss).Died += BossDie;
    }

    private void BossDie()
    {
        FightEnd?.Invoke(_friends);
    }

    private void OnDisable()
    {
        Friend.FriendDie -= RemoveFriend;
        ((IDamageable)_boss).Died -= BossDie;
    }

    private void RemoveFriend(Friend value)
    {
        _friends.Remove(value);
    }

    private void Start()
    {
        _newPosition = FindObjectOfType<TagNewStickmanPosition>()?.transform;
        _friends = new List<Friend>();
        _panelOpener = new PanelOpener();
        _collider = GetComponent<SphereCollider>();
        _collider.radius = _distantToTrigger;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stickman stickman) && _boss != null)
        {
            if (stickman as Friend)
            {
                var friend = (Friend)stickman;
                if (_friends.Contains(friend) == false)
                {
                    friend.IsTrigger = true;
                    _friends.Add(friend);
                    var randomSpeed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);
                    stickman.NavMeshAgent.speed = randomSpeed;
                    friend.Figth();
                    friend.Init(_boss.transform);
                    friend.GoToAttack(_speedByDistant);
                }
            }

            if (stickman as Player)
            {
                if (_isFigthTrigger)
                    return;
                _player = (Player)stickman;
                _player.GetComponent<NavMeshAgent>().enabled = true;
                _player.MovementSystem.enabled = false;
                DisconectTrailRenderBrush(_player);
                _player.Init(_newPosition);
                _player.Pursue();
                _isFigthTrigger = true;
                StartCoroutine(WaitIfNotDie());
            }
        }
    }

    private void DisconectTrailRenderBrush(Player player)
    {
        var brushCase = _player.GetComponent<BrushCase>();
        foreach (var brush in brushCase.Brushes)
        {
            brush.BrushColorChanger.DisconnectTrailRender();
        }
    }

    private IEnumerator WaitIfNotDie()
    {
        yield return new WaitForSeconds(1);
        yield return new WaitWhile(() => _friends.Count != 0);
        if (_boss != null)
        {
            _boss.Animator.Joi();
            yield return new WaitForSeconds(2);
            GameSoundsPlayer.Instance.PlaySound(Sound.Lose);
            _panelOpener.OpenPanel(_losePanel.gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _distantToTrigger * this.transform.localScale.x);
    }
#endif
}