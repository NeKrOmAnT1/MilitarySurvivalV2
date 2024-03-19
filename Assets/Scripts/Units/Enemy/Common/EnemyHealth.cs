using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 20;
    [SerializeField] private EnemyDeath _enemyDeath;
 
    public float CurrentHealth => _health;

    public event Action<float> OnTakeDamage;

    private float _health;

    void Awake() =>
        _health = _maxHealth;

    public void TakeDamage(float amount)
    {
        _health -= amount;

        OnTakeDamage?.Invoke(_health);

        if (_health <= 0)
        {
            _enemyDeath.Death();
        }
        else
        {
            // do something
        }
    }
}
