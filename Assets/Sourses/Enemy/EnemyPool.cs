using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class EnemyPool : MonoBehaviour
{
    private List<Enemy> _enemyPool = new List<Enemy>();
    private List<Enemy> _enemyList = new List<Enemy>();

    public event UnityAction<int> PowerBalanceChanged;

    public Stickman GetNearest(Stickman exception)
    {
        float minDistance = float.MaxValue;
        Stickman stickman = null;
        foreach (var enemy in _enemyList)
        {
            if (enemy != exception)
            {
                float distance = Vector3.Distance(exception.transform.position, enemy.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    stickman = enemy;
                }
            }
        }

        return stickman;
    }

    public Enemy GetEnemy()
    {
        Enemy enemy = _enemyPool.Where(e => e is EnemyBoss == false).FirstOrDefault();
        _enemyPool.Remove(enemy);
        return enemy;
    }

    public Enemy GetEnemyBoss()
    {
        Enemy enemy = _enemyPool.Where(e => e is EnemyBoss).FirstOrDefault();
        _enemyPool.Remove(enemy);
        return enemy;
    }

    public void Init(Transform parent, Enemy enemy, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Enemy newEnemy = Instantiate(enemy, parent);
            Init(newEnemy);
        }
    }

    public void Init(Enemy enemy)
    {
        _enemyList.Add(enemy);
        _enemyPool.Add(enemy);

        enemy.Died += OnEnemyDied;
        enemy.BecameFriend += OnBecomeFriend;
    }

    public void StopAllEnemys()
    {
        foreach (var enemy in _enemyList)
        {
            enemy.Stop();
            enemy.Joi();
        }
    }

    private int GetEnemyCount()
    {
        return _enemyList.Where(enemy => enemy.Friend == false).Count();
    }

    private void OnEnemyDied(Stickman enemy)
    {
        _enemyList.Remove(enemy as Enemy);
        enemy.Died -= OnEnemyDied;
        GameSoundsPlayer.Instance?.PlaySound(Sound.Die);

        Notyfi();
    }

    private void OnBecomeFriend(Enemy enemy)
    {
        enemy.BecameFriend -= OnBecomeFriend;

        Notyfi();
    }

    private void Notyfi()
    {
        int enemyCount = GetEnemyCount();
        if (enemyCount <= 0)
            StopAllEnemys();
        PowerBalanceChanged?.Invoke(enemyCount);
    }
}