using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] private PlayerParameter hpParameter;
    [SerializeField] private PlayerParameter armorParameter;
    [SerializeField] private PlayerParameter manaParameter;

    public static Transform Instance { get; }


    private float hp;
    [SerializeField] private float maxHp;
    private float armor;
    [SerializeField] private float maxArmor;
    private float mana;
    [SerializeField] private float maxMana;
    
    private bool facingRight = true;
    private Vector2 movement;

    private Animator animator;
    private Rigidbody2D rb;
    
    private Sword sword;
    
    
    private void Start()
    {
        hp = maxHp;
        armor = maxArmor;
        mana = maxMana;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sword = GetComponentInChildren<Sword>();
    }

    private void Update()
    {
        UpdateParameters();
        
        movement = new Vector2(
            (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0),
            (Input.GetKey(KeyCode.S) ? -1 : 0) + (Input.GetKey(KeyCode.W) ? 1 : 0));
        
        animator.SetBool("isRunning", !movement.Equals(Vector2.zero));

        if (facingRight && movement.x < -1e-8 || !facingRight && movement.x > 1e-8)
            Flip();
        
        if (Input.GetMouseButtonDown(0)) 
        {
            sword?.Attack();
        }
        
    }

    private void FixedUpdate()
    {
        rb.velocity = movement.normalized * moveSpeed;
    }

    private void UpdateParameters()
    {
        hpParameter.SetStat(hp, maxHp);
        armorParameter.SetStat(armor, maxArmor);
        manaParameter.SetStat(mana, maxMana);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        var scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void TakeDamage(float damageAmount)
    {
        hp -= damageAmount;
    }
}
