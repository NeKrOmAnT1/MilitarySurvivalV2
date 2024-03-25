using System;
using System.Collections.Generic;
using UnityEngine;


public class AmmoPool : MonoBehaviour
{ 
    [SerializeField] public List<PoolData> _ownerPoolData;
    private Dictionary< ProjectileOwner, EnemyPool<Projectile>> _poolDictionary = new();

    private void Awake()
    {       
        CreateAllPool();
    }

    public EnemyPool<Projectile> GetProjectilePool(ProjectileOwner owner)
    {
        return _poolDictionary[owner];
    }
    private void CreateAllPool()
    {
        foreach (var owner in _ownerPoolData)
        {
            GameObject parentGo = CreateGOAndSetParent(transform, owner.ProjectileOwner);

            _poolDictionary.Add(owner.ProjectileOwner, new EnemyPool<Projectile>(owner.Prefab,owner._startPoolSize, parentGo.transform));
        }
    }
    private GameObject CreateGOAndSetParent(Transform parentTransform, Enum name)
    {
        GameObject childrenGo = new GameObject($"{name}");
        childrenGo.transform.parent = parentTransform;
        return childrenGo;
    }
}