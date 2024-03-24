
using System;

public class MoneySystem
{
    public int Money { get; private set; }

    public event Action<int> MoneyChange;

    public MoneySystem(EnemySpawnManager enemySpawnManager) => 
        enemySpawnManager.OnSpawned += AddEnemy;

    private void AddEnemy(EnemyDeath enemy) =>
        enemy.OnDeadMonty += AddMoney;

    public void AddMoney(int value)
    {
        Money += value;
        MoneyChange?.Invoke(Money);
    }

    public void SpendMoney(int value)
    {
        Money -= value;
        MoneyChange?.Invoke(Money);
    }
}
