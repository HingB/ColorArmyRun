using UnityEngine;
using PathCreation.Examples;

namespace RunnerMovementSystem
{
    [RequireComponent(typeof(RoadMeshCreator))]
    public class RoadSegment : PathSegment
    {
        [field: SerializeField] public bool AutoMoveForward { get; set; } = true;

        private RoadMeshCreator _roadMesh;

        public override float Width => _roadMesh.roadWidth;

        protected override void OnAwake()
        {
            _roadMesh = GetComponent<RoadMeshCreator>();
        }
    }
}