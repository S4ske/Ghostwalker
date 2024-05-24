using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    private EnemySwordVisual enemySwordVisual;

    [SerializeField] private float damageAmount;

    private CircleCollider2D polygonCollider2D;
    [SerializeField] private Enemy enemy;

    // Start is called before the first frame update
    private void Awake()
    {
        enemySwordVisual = GetComponent<EnemySwordVisual>();
        polygonCollider2D = GetComponent<CircleCollider2D>();

    }

    private void Update()
    {
        if (!enemy.Attack)
            EnemyAttackColliderTurnOff();
    }

    // public void Attack()
    // {
    //     EnemyAttackColliderTurnOn();
    // }

    public void EnemyAttackColliderTurnOn()
    {
        polygonCollider2D.enabled = true;
    }
    public void EnemyAttackColliderTurnOff()
    {
        polygonCollider2D.enabled = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.TakeDamage(damageAmount);
        }
    }
}
