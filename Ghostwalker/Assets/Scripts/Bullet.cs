using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public float damage;
    public LayerMask whatIsSolid;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);
        if (hitInfo.collider is not null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }
}
