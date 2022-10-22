using UnityEngine;

namespace Assets.Sourses.Tag
{
    public class TagNewStickmanPosition : MonoBehaviour
    {
        private PositionCeator _positionCeator;
        [SerializeField] private int lengthRow = 5;
        [SerializeField] private float distant = 1;

        private void Start()
        {
            _positionCeator = new PositionCeator(transform.position, lengthRow, distant,
                transform.right, -transform.forward);
        }

        public Vector3 GetPosition() => _positionCeator.GetPosition();

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.right * distant * lengthRow);
            Gizmos.DrawLine(transform.position, transform.position - transform.forward * distant);
        }
#endif
    }
}
