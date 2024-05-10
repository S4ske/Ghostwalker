using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private Animator animator;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int Attack = Animator.StringToHash("Attack 0");
    private static readonly int IsDie = Animator.StringToHash("IsDie");

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log(enemy.IsRunning);
        animator.SetBool(IsRunning, enemy.IsRunning);
        animator.SetBool(Attack, enemy.Attack);
        animator.SetBool(IsDie, enemy.Death);
    }
}
