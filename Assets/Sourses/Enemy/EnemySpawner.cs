using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private BonusLevelReferee _referee;
    [SerializeField] private Transform _parent;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private EnemyPool _pool;
    [SerializeField] private EnemyProvocateur _enemyProvocateur;
    [SerializeField] private Wawe[] _wawes;
    [SerializeField] private bool _usePool = true;

    private bool _gameOver;

    public void StartWawe()
    {
        if (_usePool)
        {
            foreach (var wawe in _wawes)
                _pool.Init(_parent, wawe.Enemy, wawe.Count);
        }

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        foreach (var wawe in _wawes)
        {
            for (int i = 0; i < wawe.Count; i++)
            {
                if (_gameOver)
                    break;
                Vector3 spawnPostion = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
                Enemy enemy = null;
                if (_usePool)
                {
                    if (wawe.Enemy is EnemyBoss)
                        enemy = _pool.GetEnemyBoss();
                    else
                        enemy = _pool.GetEnemy();
                }
                else
                {
                    enemy = Instantiate(wawe.Enemy, spawnPostion, Quaternion.identity);
                    _pool.Init(enemy);
                }

                enemy.transform.position = spawnPostion;
                _enemyProvocateur.Init(enemy);
                yield return new WaitForSeconds(wawe.Delay);
            }
        }
    }

    private void StopWawe()
    {
        StopCoroutine(Spawn());
        _gameOver = true;
    }

    private void OnEnable()
    {
        _referee.Won += StopWawe;
        _referee.Lose += StopWawe;
    }

    private void OnDisable()
    {
        _referee.Won -= StopWawe;
        _referee.Lose -= StopWawe;
    }
}

[System.Serializable]
public struct Wawe
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _count;
    [SerializeField] private float _delay;

    public Enemy Enemy => _enemy;
    public int Count => _count;
    public float Delay => _delay;
}