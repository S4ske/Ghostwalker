using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyVisual : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private Animator animator;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log(enemy.IsRunning);
        animator.SetBool(IsRunning, enemy.IsRunning);
    }
}
