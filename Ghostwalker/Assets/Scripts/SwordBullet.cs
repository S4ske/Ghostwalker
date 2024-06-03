using UnityEngine;

public class SwordBullet : Bullet
{
    [SerializeField] private float existingTime;
    private float currentTime;
    private bool isAttacked;
    
    void Update()
    {
        if (currentTime >= existingTime)
            Destroy(gameObject);
        else
            currentTime += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage * (Time.deltaTime / existingTime));
        }
        if (other.CompareTag("Boss"))
        {
            other.GetComponent<Boss>().TakeDamage(damage * (Time.deltaTime / existingTime));
        }
    }
}
