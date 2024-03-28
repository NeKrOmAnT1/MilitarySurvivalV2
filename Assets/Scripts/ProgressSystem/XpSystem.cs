using System;
using UnityEngine;
using Zenject;

public class XpSystem
{
    private float _targetXp = 100; //where to get it from?
    private readonly SquadScript _squadScript;

    public float CurrentXp { get; private set; }
    public float TargetXp { get => _targetXp; }

    public float XpLevel { get; private set; }

    public event Action XpIsFull;
    public event Action ChangeXPE;

    [Inject]
    public XpSystem(EnemySpawnManager enemySpawnManager, SquadScript squadScript)
    {
        enemySpawnManager.OnSpawned += AddEnemy;

        ChangeXPE?.Invoke();
        _squadScript = squadScript;
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
            XpLevel++;

            if (XpLevel > 2 && XpLevel % 3 == 0)
                _squadScript.CanSpawn();

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
