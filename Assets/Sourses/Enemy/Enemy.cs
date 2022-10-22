using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Stickman
{
    [SerializeField] private Renderer _render;
    [SerializeField] private ParticlesController _particleController;

    public event UnityAction<Enemy> BecameFriend;
    public bool Friend { get; private set; }

    [field: SerializeField] public CaseMaterial CaseMaterial { get; private set; }
    [field: SerializeField] public CaseMaterial FriendMaterial { get; private set; }

    public void BecomeFriend()
    {
        Friend = true;
        Die();
    }

    private void Awake()
    {
        InitStickman(new StickmanEngine(), new StickmanAnimator(new EnemyDie(_particleController, _render, transform)));
        _render.material = CaseMaterial.Friend;
    }
}
