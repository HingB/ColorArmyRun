using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Assets.Sourses.Player.Bruse.Case
{
    public class BrushCaseEngine
    {
        private List<Brush> _brushes;
        private Vector3 _centerLocalPosition;
        private Dictionary<Brush, TweenerCore<Vector3, Vector3, VectorOptions>> _animationList;
        private float _timeAnimation = 1f;

        public BrushCaseEngine(Transform centerPosition, List<Brush> brushes)
        {
            _centerLocalPosition = centerPosition.localPosition;
            _brushes = brushes;
            _animationList = new Dictionary<Brush, TweenerCore<Vector3, Vector3, VectorOptions>>();
        }

        public float Range { get; set; }

        public void SetBrushPosition()
        {
            var position = CreatePoint(_brushes.Count);
            for (int i = 0; i < _brushes.Count; i++)
            {
                var moveAnimation = _brushes[i].transform.DOLocalMove(position[i], _timeAnimation);
                if (_animationList.ContainsKey(_brushes[i]))
                    RemoveAnimation(_brushes[i]);
                _animationList.Add(_brushes[i], moveAnimation);
            }
        }

        public void RemoveAnimation(Brush brush)
        {
            if (_animationList.TryGetValue(brush, out var animation))
            {
                animation.Kill();
                _animationList.Remove(brush);
            }
        }

        public List<Vector3> CreatePoint(int count)
        {
            int offcet = count / 2;
            List<Vector3> result = new List<Vector3>();
            Vector3 startPosition;
            if (_brushes.Count % 2 == 1)
                startPosition = _centerLocalPosition - (Vector3.right * Range * offcet);
            else
                startPosition = _centerLocalPosition - (Vector3.right * (Range / 2)) - (Vector3.right * (offcet - 1) * Range);

            for (int i = 0; i < count; i++)
            {
                result.Add(startPosition + (Vector3.right * Range * i));
            }

            return result;
        }
    }
}
