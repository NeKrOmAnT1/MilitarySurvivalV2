using UnityEngine;

public class MelleEnemy : Enemy, ICanAttack
{
    private float _timer = 0;
    private bool _isAttack = false;

    private void StartAttack()
    {
        MelleAttack();
        _isAttack = false;
    }

    private void MelleAttack() =>
        _playerHealth.HealthReduce(_damage);

    public void AttackProcess()
    {
        if (_enemyDeath.IsDead) return;

        _timer -= Time.deltaTime;
        if (_timer < 0f && _isAttack == false)
        {
            Debug.Log("AttackProcess");
            _isAttack = true;
            StartAttack();
            _timer = _attackDelay;
        }
    }
}
