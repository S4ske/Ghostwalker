using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage;
    private Vector3 direction;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask whatIsSolid;
    public GameObject fireType;

    private void Update()
    {
        var hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);
        if (hitInfo.collider is not null)
            Destroy(gameObject);
        
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
            if (fireType.name == "fireball-blue-tail-big")
                other.GetComponent<Player>().moveSpeed = 3;
            Destroy(gameObject); 
        }
    }
}