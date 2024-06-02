using UnityEngine;

public class SwordVisual : MonoBehaviour
{
     [SerializeField] private Sword sword;
     private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation()
    {
        animator?.SetTrigger("Attack");
    }
    
    public void TriggerEndAttackAnimation()
    {
        sword.AttackColliderTurnOff();
    }
}