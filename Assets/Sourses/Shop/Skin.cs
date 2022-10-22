using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private Vector3 _spawnPositon;

    public Vector3 SpawnPosition => _spawnPositon;
}