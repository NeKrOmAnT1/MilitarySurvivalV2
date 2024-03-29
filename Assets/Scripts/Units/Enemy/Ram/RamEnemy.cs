using System.Collections;
using UnityEngine;

public class RamEnemy : Enemy, ICanAttack
{
    [SerializeField] private float _chargeSpeed;

    private Vector3 direc;
    private Vector3 initialPosition;

    private bool isCharging = false;
    private bool isCoolingDown = false;
    private bool isDirecSwap = false;

    private IEnumerator ChargeAttack()
    {
        isCharging = true;
        initialPosition = new Vector3(Target.position.x, 1f, Target.transform.position.z);
        float initialDistance = Vector3.Distance(Target.position, transform.position);
        yield return new WaitForSeconds(1f);
        direc = new Vector3(Target.transform.position.x, 1f, Target.transform.position.z);
        while (true)
        {
            float distance = Vector3.Distance(Target.position, transform.position);
            if (distance <= 2f)
            {
                break;
            }
            else if (distance > initialDistance)
            {
                isDirecSwap = true;
                break;
            }

            if (direc == transform.position)
            {
                break;
            }

            if (Target.transform.position != initialPosition)
            {
                isDirecSwap = false;
            }
            if (isDirecSwap)
            {
                direc = new Vector3(Target.position.x, 1f, Target.transform.position.z);
            }
            transform.position = Vector3.MoveTowards(transform.position, direc, _chargeSpeed * Time.deltaTime);
            _enemyMovement.enabled = false;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        isCharging = false;
        _enemyMovement.enabled = true;
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(_attackDelay);
        isCoolingDown = false;
    }

    private float GetDistance()
    {
        return Vector3.Distance(transform.position, Target.transform.position);
    }

    public void AttackProcess()
    {
        if (_enemyDeath.IsDead) return;

        if (!isCharging && !isCoolingDown)
        {
            StartCoroutine(ChargeAttack());
            _enemyMovement.enabled = true;
        }

        if (GetDistance() <= 2f)
        {
            _playerHealth.HealthReduce(_damage);
        }
    }
}
