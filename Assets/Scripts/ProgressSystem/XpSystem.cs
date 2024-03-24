using System;
using UnityEngine;

public class XpSystem 
{
    private int _targetXp = 100; //where to get it from?

    public int CurrentXp { get; private set; }
    public int TargetXp { get => _targetXp; }

    public event Action XpIsFull;
    public event Action ChangeXPE;


    public XpSystem(EnemySpawnManager enemySpawnManager)
    {
        enemySpawnManager.OnSpawned += AddEnemy;
        
        ChangeXPE?.Invoke();
        Debug.Log(CurrentXp);
        Debug.Log(_targetXp);
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
        }

        ChangeXPE?.Invoke();
    }

    public void Reset()
    {
        CurrentXp = 0;
        ChangeXPE?.Invoke();
        _targetXp += 10; //trmoirary
    }
}
