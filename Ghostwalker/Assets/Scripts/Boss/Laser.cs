using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float damage;

    public void RotateAroundBoss(Transform bossTransform)
    {
        transform.SetParent(bossTransform); 
    }

    private void Update()
    {
        transform.RotateAround(transform.parent.position, Vector3.forward, rotateSpeed * Time.deltaTime); 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject); 
        }
    }
}