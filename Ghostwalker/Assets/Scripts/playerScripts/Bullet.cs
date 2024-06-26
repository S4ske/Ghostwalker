using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public float damage;
    public LayerMask whatIsSolid;
    
    void Update()
    {
        var hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);
        if (hitInfo.collider is not null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }

            if (hitInfo.collider.CompareTag("Boss"))
            {
                hitInfo.collider.GetComponent<Boss>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }
}
