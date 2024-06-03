using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] private PlayerParameter hpParameter;
    [SerializeField] private PlayerParameter armorParameter;
    [SerializeField] private PlayerParameter manaParameter;
    public bool Listened;

    public static Transform Instance { get; }

    public bool WithMantle;
    public bool pressedE;
    private float eCd = 0.1f;
    public float currentECd;

    private float hp;
    [SerializeField] private float maxHp;
    private float armor;
    [SerializeField] private float maxArmor;
    public float mana;
    [SerializeField] private float maxMana;
    
    private bool facingRight = true;
    private Vector2 movement;
    
    private Rigidbody2D rb;
    
    private Sword sword;
    
    
    private void Start()
    {
        hp = maxHp;
        armor = maxArmor;
        mana = maxMana;
        rb = GetComponent<Rigidbody2D>();
        sword = GetComponentInChildren<Sword>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            pressedE = true;
            currentECd = eCd;
        }

        if (pressedE)
        {
            currentECd -= Time.deltaTime;
            if (currentECd <= 0)
                pressedE = false;
        }
        UpdateParameters();
        
        if (!Listened)
            return;
        
        movement = new Vector2(
            (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0),
            (Input.GetKey(KeyCode.S) ? -1 : 0) + (Input.GetKey(KeyCode.W) ? 1 : 0));

        if (facingRight && movement.x < -1e-8 || !facingRight && movement.x > 1e-8)
            Flip();
        
        if (Input.GetMouseButtonDown(0)) 
        {
            sword?.Attack();
        }

        if (!Listened)
            movement = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (!Listened)
            return;
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
        if (!Listened)
            return;
        var hpDamage = damageAmount - armor;
        if (hpDamage < 0)
            hpDamage = 0;
        armor -= damageAmount;
        if (armor < 0)
            armor = 0;
        hp -= hpDamage;
        if (hp <= 0)
            SceneManager.LoadScene("GameOver");
    }

    public void GetArmor(int heal)
    {
        armor += heal;
        if (armor > maxArmor)
            armor = maxArmor;
    }
    
    public void GetMana(int heal)
    {
        mana += heal;
        if (mana > maxMana)
            mana = maxMana;
    }
}
