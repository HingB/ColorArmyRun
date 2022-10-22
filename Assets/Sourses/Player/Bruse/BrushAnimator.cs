using RunnerMovementSystem.Examples;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BrushAnimator : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _hightBrush = 2;
    private GameMouseInput _mouseInput;
    private Player _player;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = FindObjectOfType<Player>();
        _mouseInput = FindObjectOfType<GameMouseInput>();
    }

    private void FixedUpdate()
    {
        if (_mouseInput.IsMoved)
            _rigidbody.velocity = (-_player.NormalizeDirection * _speed) + (Vector3.down * _hightBrush);
        else
            _rigidbody.velocity = Vector3.zero;
    }
}
