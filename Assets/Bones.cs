using UnityEngine;
using UnityEngine.Events;

public class Bones : MonoBehaviour
{
    public static event UnityAction<Transform> BonesAdded;
    public static event UnityAction<Transform> BonesRemoved;

    private void Start()
    {
        BonesAdded?.Invoke(transform);
    }

    private void OnDisable()
    {
        BonesRemoved?.Invoke(transform);
    }
}
