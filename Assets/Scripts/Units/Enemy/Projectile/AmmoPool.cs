using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class AmmoPool : MonoBehaviour
{ 
    //[SerializeField] private GameObject _projectilePrefab;
    //[SerializeField] private int _bulletsPoolSize = 20;
    //[SerializeField] private int startPoolSize = 10;

    [SerializeField] public List<OwnerPoolData> _ownerPoolData;
    private Dictionary<ProjectileOwner, Dictionary<ProjectileOwner, ObjectPool<Projectile>>> _poolDictionary = new ();



    [SerializeField] private ProjectileOwner _owner => ProjectileOwner.ProjectileEnemy;
    //public EnemyPool<Projectile> _projectilePool;

    //private void Awake()
    //{
    //    //InitProjectilePool();
    //    //Debug.Log(GetProjectilePool());
    //}

    //public EnemyPool<Projectile> GetProjectilePool()
    //{
    //    return _projectilePool;
    //}

    //private void InitProjectilePool()
    //{
    //    GameObject projectileContainer = new GameObject("ProjectileContainer");
    //    _projectilePool = new EnemyPool<Projectile>(_projectilePrefab.GetComponent<Projectile>(), _bulletsPoolSize, projectileContainer.transform);
    //}


    private void Awake()
    {
        CreateAllPools();
    }

    public ObjectPool<Projectile> GetPool(ProjectileOwner owner, ProjectileOwner typeDamage)
    {
        return _poolDictionary[owner][typeDamage];
    }

    private void CreateAllPools()
    {
        foreach (var owner in _ownerPoolData)
        {
            GameObject parentGo = CreateGOAndSetParent(transform, owner.ProjectileOwner);

            _poolDictionary.Add(owner.ProjectileOwner, new());

            foreach (var pool in owner.Pools)
            {
                GameObject childrenGo = CreateGOAndSetParent(parentGo.transform, owner.ProjectileOwner);

                _poolDictionary[owner.ProjectileOwner].Add(owner.ProjectileOwner, CreatePool(pool, childrenGo.transform, owner.ProjectileOwner));
            }
        }
    }
  
    private GameObject CreateGOAndSetParent(Transform parentTransform, Enum name)
    {
        GameObject childrenGo = new GameObject($"{name}");
        childrenGo.transform.parent = parentTransform;
        return childrenGo;
    }

    private ObjectPool<Projectile> CreatePool(ProjectilePoolData data, Transform container, ProjectileOwner owner)
    {     
        ObjectPool<Projectile> projectilePool = null;
        ObjectPool<Projectile> pool = new(
                    () =>
                    {
                        Projectile projectile = Instantiate(data.Prefab, container);                      
                        projectile.ProjectilePool = projectilePool;
                        return projectile;
                    },
                    projectileGet =>
                    {
                        projectileGet.gameObject.SetActive(true);
                    },
                    projectileRelease =>
                    {
                        projectileRelease.gameObject.SetActive(false);
                    },
                    projectileDestroy =>
                    {
                        Destroy(projectileDestroy.gameObject);
                    },
                    false,
                    data.PoolCount,
                    data.PoolMaxCount
                );
        projectilePool = pool;

        return projectilePool;
    }
}