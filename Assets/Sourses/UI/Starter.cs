using RunnerMovementSystem;
using RunnerMovementSystem.Examples;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private RoadSegment _movementSystem;
    [SerializeField] private GameMouseInput _mouseInput;
    [SerializeField] private GameObject _startImage;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _mouseInput.IsStartMoved += StartMove;
        _movementSystem.AutoMoveForward = false;
        _startImage = FindObjectOfType<TapToPlayButton>().gameObject;
    }

    private void OnDisable()
    {
        _mouseInput.IsStartMoved -= StartMove;
    }

    private void StartMove()
    {
        _animator.Play(AnimatorStickmanController.Trigger.Run);
        _startImage.SetActive(false);
    }
}
