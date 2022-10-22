using RunnerMovementSystem;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MovementSystem))]
public class Player : Stickman
{
    [SerializeField] private CaseMaterial _caseMaterial;
    private float _sqrDistansOverwriting = 0.1f;
    private Skin _currentSkin;

    public static event UnityAction<CaseMaterial> MaterialChanged;
    public static event UnityAction<Skin> SkinChanged;
    public MovementSystem MovementSystem { get; private set; }
    public Vector3 LastPosition { get; private set; }
    public Vector3 NormalizeDirection { get; private set; }

    private void Start()
    {
        MovementSystem = this.GetComponent<MovementSystem>();
        ChangeMaterials(_caseMaterial);
        InitStickman(new StickmanEngine(), new StickmanAnimator(new DefaultDie()));
    }

    private void Update()
    {
        Vector3 direction = transform.position - LastPosition;
        var sqrMagnitude = direction.sqrMagnitude;
        if (sqrMagnitude > _sqrDistansOverwriting)
        {
            LastPosition = transform.position;
            NormalizeDirection = direction / Mathf.Sqrt(sqrMagnitude);
        }
    }

    public void ChangeMaterials(CaseMaterial caseMaterial)
    {
        _caseMaterial = caseMaterial;
        MaterialChanged?.Invoke(_caseMaterial);
    }

    public void ChangeSkin(Skin newSkin)
    {
        _currentSkin = newSkin;
        SkinChanged?.Invoke(newSkin);
    }
}
