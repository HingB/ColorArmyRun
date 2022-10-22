using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private EnemyPool _pool;
    [SerializeField] private GameObject _panel;

    private void OnEnemysGone()
    {
        StartCoroutine(AwaitAndOpen());
    }

    private IEnumerator AwaitAndOpen(float delay = 1)
    {
        yield return new WaitForSeconds(delay);
        _panel.SetActive(true);
    }

    private void OnEnable()
    {
        //_pool.EnemysIsGone += OnEnemysGone;
    }

    private void OnDisable()
    {
        //_pool.EnemysIsGone -= OnEnemysGone;
    }
}
