using RunnerMovementSystem.Examples;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class Friend : Stickman, IPaintable
{
    [SerializeField] private Transform _hatParent;
    [SerializeField] private GameObject _material;
    [SerializeField] private Material _fire;
    [SerializeField] private ParticlesController _particleController;

    private Renderer _render;
    private bool isTrigger = false;
    private Skin _spawnedSkin;
    private Skin _currentSkin;
    private float _heatHealth;

    public static event UnityAction<Friend> FriendDie;
    public static event UnityAction<Friend> FriendFight;
    public CaseMaterial CaseMaterial { get; private set; }
    public bool Follow { get; set; } = false;
    public bool IsTrigger
    {
        get => isTrigger; set
        {
            if (value != isTrigger)
            {
                isTrigger = value;
                ChangeParent();
            }
        }
    }

    public void InstantFireMaterial()
    {
        _render.material = new Material(_fire);
        _render.material.color = CaseMaterial.DissolveMainColor;
        _render.material.SetColor("_DissolveColor", CaseMaterial.DissolveColor);
    }

    public void LeaveFireAcion()
    {
        StartCoroutine(ReturnHealth());
    }

    public void SetDissolveAmount(float currentHealth, float maxHealth = 100)
    {
        if (_render == null)
            return;
        _render.material.SetFloat("_DissolveAmount", 1 - (currentHealth / maxHealth));
        _heatHealth = currentHealth;
    }

    public Skin TrySpawnSkin()
    {
        if (_currentSkin == null)
            return null;

        _spawnedSkin = Instantiate(_currentSkin, _hatParent);
        return _spawnedSkin;
    }

    private IEnumerator ReturnHealth(float tick = 0.05f, float maxHealth = 100)
    {
        for (int i = (int)_heatHealth; i < maxHealth; i += 5)
        {
            SetDissolveAmount(i);
            yield return new WaitForSeconds(tick);
        }

        ChangeColor();
    }

    private void Awake()
    {
        _render = _material.GetComponent<Renderer>();
        InitStickman(
            new StickmanEngine(),
            new StickmanAnimator(
                new FriendDie(_particleController, _render, this.transform),
                new FriendRun()));
    }

    private void OnEnable()
    {
        Player.MaterialChanged += ChangeMaterial;
        Player.SkinChanged += OnSkinChanged;
        GameMouseInput.IsMovedChanged += ChangeAimation;
    }

    private void OnDisable()
    {
        Player.MaterialChanged -= ChangeMaterial;
        Player.SkinChanged -= OnSkinChanged;
        GameMouseInput.IsMovedChanged -= ChangeAimation;
    }

    private void OnSkinChanged(Skin skin)
    {
        _currentSkin = skin;

        if (_spawnedSkin != null)
            Destroy(_spawnedSkin.gameObject);
        if (Follow)
            TrySpawnSkin();
    }

    private void ChangeAimation(bool value)
    {
        if (Follow && IsTrigger == false)
        {
            if (value)
                Animator.Move();
            else
                Animator.Stay();
        }
    }

    private void ChangeMaterial(CaseMaterial caseMaterial)
    {
        CaseMaterial = caseMaterial;
        if (Follow == false)
            return;
    }

    public void ChangeColor()
    {
        _render.material = CaseMaterial.Friend;
    }

    private void OnDestroy()
    {
        GameSoundsPlayer.Instance?.PlaySound(Sound.Die);
        Friend.FriendDie?.Invoke(this);
    }

    public void Figth()
    {
        GetComponent<NavMeshAgent>().enabled = true;
        FriendFight?.Invoke(this);
    }

    private void ChangeParent()
    {
        var parents = transform.GetComponentsInParent<Transform>();
        var mainParent = parents[parents.Length - 1];
        transform.parent = mainParent;
    }
}
