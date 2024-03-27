using System;
using UnityEngine;

public class XpSystem
{
    private float _targetXp = 100; //where to get it from?

    public float CurrentXp { get; private set; }
    public float TargetXp { get => _targetXp; }

    public event Action XpIsFull;
    public event Action ChangeXPE;


    public XpSystem(EnemySpawnManager enemySpawnManager)
    {
        enemySpawnManager.OnSpawned += AddEnemy;

        ChangeXPE?.Invoke();
    }

    private void AddEnemy(EnemyDeath enemy) =>
        enemy.OnDeadXP += AddXP;

    private void AddXP(int xpforKilling)
    {
        CurrentXp += xpforKilling;

        if (CurrentXp >= _targetXp)
        {
            CurrentXp = _targetXp;
            XpIsFull?.Invoke();

            Reset();
        }
        ChangeXPE?.Invoke();
    }

    private void Reset()
    {
        CurrentXp = 0;
        _targetXp += 10; //trmoirary
        ChangeXPE?.Invoke();
    }
}
