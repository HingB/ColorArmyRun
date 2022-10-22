using UnityEngine;
using UnityEngine.Events;

namespace RunnerMovementSystem.Examples
{
    public class GameMouseInput : MonoBehaviour
    {
        [SerializeField] private MovementSystem _roadMovement;
        [SerializeField] private float _sensitivity = 0.01f;

        public static event UnityAction<bool> IsMovedChanged;
        private Vector3 _mousePosition;
        private float _saveOffset;
        private bool _blocked = false;

        public event UnityAction IsStartMoved;
        public event UnityAction IsStopMoved;
        public event UnityAction IsContinueMoved;
        public event UnityAction IsFinishMovement;

        public bool IsMoved { get; private set; }
        public bool IsMoveNotStarted = true;

        private void OnEnable()
        {
            _roadMovement.PathChanged += OnPathChanged;
        }

        private void OnDisable()
        {
            _roadMovement.PathChanged -= OnPathChanged;
        }

        public void Lock()
        {
            _blocked = true;
        }

        public void UnLock()
        {
            _blocked = false;
        }

        private void OnPathChanged(PathSegment _)
        {
            _saveOffset = _roadMovement.Offset;
            _mousePosition = Input.mousePosition;
        }

        private void Update()
        {
            if (_blocked)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                _saveOffset = _roadMovement.Offset;
                _mousePosition = Input.mousePosition;
                IsMoved = true;
                IsMovedChanged?.Invoke(IsMoved);
                IsContinueMoved?.Invoke();
                if (IsMoveNotStarted)
                {
                    IsStartMoved?.Invoke();
                    IsMoveNotStarted = false;
                }
            }

            if (Input.GetMouseButton(0))
            {
                var offset = Input.mousePosition - _mousePosition;
                _roadMovement.SetOffset(_saveOffset + (offset.x * _sensitivity));
                _roadMovement.MoveForward();
            }

            if (Input.GetMouseButtonUp(0))
            {
                IsMoved = false;
                IsMovedChanged?.Invoke(IsMoved);
                IsStopMoved?.Invoke();
            }
        }
    }
}