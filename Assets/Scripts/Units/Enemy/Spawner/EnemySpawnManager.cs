using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawnManager : MonoBehaviour
{

    public event Action<EnemyHealth> OnSpawned;

    [Header("EnemySettings")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private StepSpawnConfig[] stepSpawnConfigs;

    [Inject] private Player _player;
    [Inject] private ProjectileFactory _projectileFactory;

    private Transform target;
    private PlayerHealth playerHealth;

    private Dictionary<string, EnemyPool<Transform>> enemyPools;
    private int currentStep = 0;
    public int startPoolSize = 10;

    private const string SOPath = "DefaultSO/DefaultPlayerSO";
    private PlayerCharacteristics _characteristics;

    private void Start()
    {
        target = _player.transform;
        playerHealth = _player.PlayerHealth;


        enemyPools = new Dictionary<string, EnemyPool<Transform>>();
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            GameObject enemyContainer = new GameObject(enemyPrefabs[i].tag + "_Container");
            enemyPools.Add(enemyPrefabs[i].tag, new EnemyPool<Transform>(enemyPrefabs[i].GetComponent<Transform>(),
                                                                      startPoolSize,
                                                                      enemyContainer.transform));
            // Debug.Log("Нажмите Space для следующей волны противников");
        }


        StartNextSpawnStep();
    }

    private PlayerCharacteristics DefaultCharacteristicsImplementation() =>
        _characteristics = new(Instantiate(Resources.Load<PlayerSO>(SOPath)));

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
        List<string> enemyTypes = new List<string>();

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
                Debug.LogWarning("неверный тип врагов: " + enemyType);
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

        if (Enemy.TryGetComponent<ProjectileEnemy>(out var projectileEnemy)) //crutch
            projectileEnemy.DetFactory(_projectileFactory);

        OnSpawned?.Invoke(Enemy.GetComponent<EnemyHealth>());
    }
    private Transform GetRandomSpawnPoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
    }
}