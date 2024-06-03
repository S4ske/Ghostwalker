using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PolygonCollider2D polygonCollider2D;
    [SerializeField] private int damageAmount;
    private SwordVisual swordVisual;

    private void Awake()
    {
        swordVisual = GetComponentInChildren<SwordVisual>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    public void AttackColliderTurnOff() => polygonCollider2D.enabled = false;

    private void AttackColliderTurnOn() => polygonCollider2D.enabled = true;

    private void Start()
    {
        AttackColliderTurnOff();
    }

    private void AttackColliderTurnOffOn()
    {
        AttackColliderTurnOff();
        AttackColliderTurnOn();
    }

    public void Attack()
    {
        AttackColliderTurnOffOn();
        swordVisual?.PlayAttackAnimation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damageAmount);
        }
        
        if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<Boss>().TakeDamage(damageAmount);
        }
    }
}