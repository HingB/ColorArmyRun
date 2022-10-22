#if UNITY_EDITOR
using PathCreation;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(PathCreator))]
public class СonstellatorByPath : MonoBehaviour
{
    private Transform[] _transform;
    private PathCreator _pathCreator;

    private void Start()
    {
        this.enabled = false;
    }

    private void Update()
    {
        _transform = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            _transform[i] = transform.GetChild(i);
        _pathCreator = this.GetComponent<PathCreator>();

        if (_transform.Length == 0)
            return;

        float count = _pathCreator.path.length / (_transform.Length - 1);

        for (int i = 0; i < _transform.Length; i++)
        {
            float distant;
            if (_transform.Length - 1 == i)
                distant = _pathCreator.path.length - 0.01f;
            else
                distant = count * i;

            _transform[i].position = _pathCreator.path.GetPointAtDistance(distant);
        }
    }
}
#endif
