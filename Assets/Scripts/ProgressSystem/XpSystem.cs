using System;
using UnityEngine;
using Zenject;

public class XpSystem 
{
    //private HUD _hud;
    
    private readonly int _targetXp = 10; //where to get it from?

    public int CurrentXp { get; private set; }
    //public bool IsFull { get; private set; }

    public event Action XpIsFull;
    public event Action<int, int> ChangeXPE;

    public XpSystem(EnemySpawnManager enemySpawnManager)//, HUD hud)//, int targetXp)
    {
        //_targetXp = targetXp;
        //_hud = hud;
        enemySpawnManager.OnSpawned += AddEnemy;
        //_hud.UpdateXPValue(CurrentXp, _targetXp);
        ChangeXPE?.Invoke(CurrentXp, _targetXp);
    }

    private void AddEnemy(EnemyDeath enemy) =>
        enemy.OnDead += AddXP;

    private void AddXP(int xpforKilling)
    {
        CurrentXp += xpforKilling;

        if (CurrentXp >= _targetXp)
        {
            CurrentXp = _targetXp;
            //IsFull = true;
            XpIsFull?.Invoke();
        }

        //_hud.UpdateXPValue(CurrentXp, _targetXp);
        ChangeXPE?.Invoke(CurrentXp, _targetXp);
    }
}
