using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float _attackRange = 3.0f;

    public float AttackRange => _attackRange;
    private bool _gameStarted;

    private List<Enemy> _enemies; //공격범위 안에 들어오는 적들을 리스트로 저장

    public Enemy CurrentEnemyTarget { get; set; } //리스트 중 제일 첫번째 놈
    public TurretUpgrade TurretUpgrade { get; set; }

    private void Start()
    {
        TurretUpgrade = GetComponent<TurretUpgrade>();
        _gameStarted = true;
        _enemies = new List<Enemy>();
    }

    private void Update()
    {
        GetCurrentEnemyTarget();
        RotateTowardsTarget();
    }

    private void OnDrawGizmos()
    {
        if (!_gameStarted)
        {
            GetComponent<CircleCollider2D>().radius = _attackRange;
        }
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))  
        {
            Enemy newEnemy = collision.GetComponent<Enemy>();
            _enemies.Add(newEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy Enemy = collision.GetComponent<Enemy>();
            if (_enemies.Contains(Enemy))
            {
                _enemies.Remove(Enemy);
            }
        }
    }

    private void GetCurrentEnemyTarget()
    {
        if(_enemies.Count <= 0)
        {
            CurrentEnemyTarget = null;
            return;
        }
        CurrentEnemyTarget = _enemies[0];
    }

    private void RotateTowardsTarget()
    {
        if(CurrentEnemyTarget == null)
        {
            return;
        }
        Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

}
