
using System;

public class MoneySystem
{
    public float Money { get; private set; }

    public event Action<float> MoneyChange;

    public MoneySystem(EnemySpawnManager enemySpawnManager) => 
        enemySpawnManager.OnSpawned += AddEnemy;

    private void AddEnemy(EnemyDeath enemy) =>
        enemy.OnDeadMonty += AddMoney;

    public void AddMoney(float value)
    {
        Money += value;
        MoneyChange?.Invoke(Money);
    }

    public void SpendMoney(float value)
    {
        Money -= value;
        MoneyChange?.Invoke(Money);
    }
}
