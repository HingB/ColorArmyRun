using PathCreation;
using UnityEngine;

[RequireComponent(typeof(PathCreator))]
public class PathMover : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private float speed =1;
    [SerializeField] private EndOfPathInstruction _end;
    private PathCreator _pathCreator;
    private float dstTravelled;

    private void Start()
    {
        if (transform.childCount != 1)
            Debug.LogWarning("Ошибка, Может двигать только 1 дочерний объект");
        _transform = transform.GetChild(0);
        _pathCreator = GetComponent<PathCreator>(); 
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        dstTravelled += speed * Time.deltaTime;
        _transform.position = _pathCreator.path.GetPointAtDistance(dstTravelled, _end);
    }
}
