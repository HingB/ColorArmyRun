using System;
using UnityEngine;

public class EnemyProvocateur : MonoBehaviour
{
    [SerializeField] private Transform _firstTarget;
    [SerializeField] private EnemyPool _pool;

    public void StartFigth(Stickman first, Stickman second)
    {
        if (second == null)
            return;
    }

    public void Init(Enemy enemy)
    {
        enemy.BecameFriend += OnBecameFriend;
        enemy.TargetDestroyed += OnTargetDestroyed;
        enemy.Died += OnEnemyDied;
        AttackFirstTarget(enemy);
    }

    private void AttackFirstTarget(Enemy enemy)
    {
        enemy.Init(_firstTarget);
        enemy.GoToAttack();
    }

    private void AtackSameEnemy(Stickman first, Stickman second)
    {
        first.Init(second.transform);
        first.GoToAttack();
        second.Init(first.transform);
        second.GoToAttack();
    }

    private void AttackWithPover(EnemyBoss boss, Stickman second)
    {
        second.Init(boss.transform);
        second.GoToAttack();
        boss.Init(second.transform);
        boss.LookAt();
        boss.Pursue();
    }

    private void OnTargetDestroyed(Stickman stickman)
    {
        if (stickman is EnemyBoss boss)
        {
            if (boss.Friend)
                StartFigth(stickman, _pool.GetNearest(stickman));
            else
                AttackFirstTarget(boss);
        }
        else if (stickman is Enemy enemy)
        {
            if (enemy.Friend == false)
                AttackFirstTarget(enemy);
        }
    }

    private void OnEnemyDied(Stickman stickman)
    {
        stickman.Died -= OnEnemyDied;
    }

    private void OnBecameFriend(Enemy enemy)
    {
        enemy.BecameFriend -= OnBecameFriend;
        StartFigth(enemy, _pool.GetNearest(enemy));
    }
}
