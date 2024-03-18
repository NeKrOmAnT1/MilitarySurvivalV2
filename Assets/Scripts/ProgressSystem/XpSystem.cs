
using UnityEngine;

public class XpSystem 
{
    private readonly int _targetXp; //where to get it from?

    public int Xp { get; private set; }
    public bool IsFull { get; private set; }

    /*public XpSystem(EnemySpawnManager enemySpawnManager, int targetXp)
    {
        _targetXp = targetXp;
        //enemySpawnManager.OnSpawned += AddEnemy;

        Debug.Log(enemySpawnManager);
        Debug.Log(_targetXp); 
    }*/

    //private void AddEnemy(EnemyHealth enemyHealth) => 
    //    enemyHealth.OnDead += AddXP;

    private void AddXP(int xpforKilling)
    {
        Xp += xpforKilling;

        if (Xp >= _targetXp)
        {
            Xp = _targetXp;
            IsFull = true;
        }
    }
}
