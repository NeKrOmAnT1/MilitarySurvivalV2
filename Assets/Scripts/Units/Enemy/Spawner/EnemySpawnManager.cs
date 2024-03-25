using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawnManager : MonoBehaviour
{
    public event Action<EnemyDeath> OnSpawned;

    [Header("EnemySettings")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private StepSpawnConfig[] stepSpawnConfigs;

    private Player _player;

    private Transform target;
    private PlayerHealth playerHealth;

    private Dictionary<string, EnemyPool<Transform>> enemyPools;
    private int currentStep = 0;
    public int startPoolSize = 10;



    //private EnemyPool<Projectile> _pool;
    //[SerializeField] private GameObject _projectilePrefab;
    //[SerializeField] private int _bulletsPoolSize = 20;


    

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
        target = _player.transform;
        playerHealth = _player.PlayerHealth;
    }


    //public EnemyPool<Projectile> GetPool()
    //{   
    //    return _pool;
    //}

    private void Start()
    {
        InitEnemyPool();

        //InitProjectilePool();

        StartNextSpawnStep();
    }

    //private void InitProjectilePool()
    //{
    //    GameObject projectileContainer = new("ProjectileContainer");
    //    _pool = new EnemyPool<Projectile>(_projectilePrefab.GetComponent<Projectile>(), _bulletsPoolSize, projectileContainer.transform);
    //}

    private void InitEnemyPool()
    {
        enemyPools = new Dictionary<string, EnemyPool<Transform>>();
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            GameObject enemyContainer = new(enemyPrefabs[i].tag + "_Container");
            enemyPools.Add(enemyPrefabs[i].tag, new EnemyPool<Transform>(enemyPrefabs[i].GetComponent<Transform>(),
                                                                      startPoolSize,
                                                                      enemyContainer.transform));
        }
    }


    private void StartNextSpawnStep()
    {
        if (currentStep < stepSpawnConfigs.Length)
        {
            StartCoroutine(SpawnEnemiesOnStep(stepSpawnConfigs[currentStep]));
            currentStep++;
        }
    }

    private IEnumerator SpawnEnemiesOnStep(StepSpawnConfig stepConfig)
    {
        float totalEnemiesToSpawn = stepConfig.GetAllAmountEnemy();
        List<string> enemyTypes = new();

        for (int i = 0; i < stepConfig.melee1EnemyAmount; i++)
            enemyTypes.Add("Melee1");
        for (int i = 0; i < stepConfig.ram1EnemyAmount; i++)
            enemyTypes.Add("Ram1");
        for (int i = 0; i < stepConfig.spit1EnemyAmount; i++)
            enemyTypes.Add("Spit1");
        for (int i = 0; i < stepConfig.projectile1EnemyAmount; i++)
            enemyTypes.Add("Projectile1");

        enemyTypes.Shuffle();

        float spawnDelay = stepConfig.spawnTime / totalEnemiesToSpawn;

        foreach (string enemyType in enemyTypes)
        {
            if (enemyPools.TryGetValue(enemyType, out EnemyPool<Transform> pool))
            {
                SpawnEnemyFromPool(pool);
                yield return new WaitForSeconds(spawnDelay);
            }
            else
            {
                Debug.LogWarning("�������� ��� ������: " + enemyType);
            }
        }


        yield return new WaitForSeconds(stepConfig.spawnTime);
        StartNextSpawnStep();
    }

    private void SpawnEnemyFromPool(EnemyPool<Transform> pool)
    {
        Transform randomSpawnPoint = GetRandomSpawnPoint();
        Transform enemy = pool.GetObject();
        enemy.position = randomSpawnPoint.position;
        var Enemy = enemy.GetComponent<Enemy>();
        Enemy.Init(playerHealth, target);

        OnSpawned?.Invoke(Enemy.GetComponent<EnemyDeath>());
    }
    private Transform GetRandomSpawnPoint() =>
        spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
}