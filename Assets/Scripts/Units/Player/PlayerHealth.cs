using System;
using UnityEngine;

public class PlayerHealth
{
    private float _currentHealth;
    private readonly Player _player;

    public Action<float, float> OnHealthChangedE;//for UI

    public PlayerHealth(Player player)
    {
        _player = player;
        _currentHealth = _player.PlayerCharacteristics.Hp.Value;

        _player.PlayerCharacteristics.ChangeHPE += ChangeMaxHP;
    }

    public void HealthReduce(float damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Debug.Log("You Dead!");

                //TODO class playerDeath
            }

            OnHealthChangedE?.Invoke(_currentHealth, _player.PlayerCharacteristics.Hp.Value);//for UI
        }
    }

    public void HealthRepare(float value)
    {
        _currentHealth += value;
        if (_currentHealth >= _player.PlayerCharacteristics.Hp.Value)
            _currentHealth = _player.PlayerCharacteristics.Hp.Value;

        OnHealthChangedE?.Invoke(_currentHealth, _player.PlayerCharacteristics.Hp.Value);//for UI
    }

    private void ChangeMaxHP() =>
        OnHealthChangedE?.Invoke(_currentHealth, _player.PlayerCharacteristics.Hp.Value);
}