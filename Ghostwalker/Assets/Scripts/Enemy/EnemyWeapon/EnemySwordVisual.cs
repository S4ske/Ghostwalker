using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class EnemySwordVisual : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemySword enemySword;

    private string CanAttack = "CanAttack";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(CanAttack, enemy.health > 0 && enemy.Attack);
    }
    
    public void TriggerEndAttackAnimation()
    {
        enemySword.EnemyAttackColliderTurnOff();
    }
    public void TriggerStartAttackAnimation()
    {
        enemySword.EnemyAttackColliderTurnOn();
    }
}
