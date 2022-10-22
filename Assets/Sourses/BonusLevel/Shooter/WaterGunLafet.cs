using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunLafet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameResults _results;

    public void LookAt(Vector3 offset)
    {
        if (_results.Ended)
            return;


    }
}