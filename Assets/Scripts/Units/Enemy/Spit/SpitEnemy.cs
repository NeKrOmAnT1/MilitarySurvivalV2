using UnityEngine;

public class SpitEnemy : Enemy, ICanAttack
{
    [SerializeField] private GameObject SpitPrefab;

    private bool _isAttack = false;
    private float _timer = 0;

    private void SpittingAttack()
    {
        // Replace "SpitPrefab" with the actual prefab you want to instantiate
        //Use pulls and factories in the same way as a bullet
        GameObject spitObject = Instantiate(SpitPrefab, transform.position, Quaternion.identity);
        Spit spitComponent = spitObject.GetComponent<Spit>();

        spitComponent.Initialize(_playerHealth, Target.position, _damage);
        _isAttack = false;
    }

    public void AttackProcess()
    {
        if (_enemyDeath.IsDead) return;

        _timer -= Time.deltaTime;
        if (_timer < 0f && _isAttack == false)
        {
            _isAttack = true;
            SpittingAttack();
            _timer = _attackDelay;
        }
    }
}
