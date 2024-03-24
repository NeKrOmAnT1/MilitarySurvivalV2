using System;

public class XpSystem 
{
    private readonly int _targetXp = 10; //where to get it from?

    public int CurrentXp { get; private set; }

    public event Action XpIsFull;
    public event Action<int, int> ChangeXPE;

    public XpSystem(EnemySpawnManager enemySpawnManager)
    {
        enemySpawnManager.OnSpawned += AddEnemy;
        
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
            XpIsFull?.Invoke();
        }

        ChangeXPE?.Invoke(CurrentXp, _targetXp);
    }
}
