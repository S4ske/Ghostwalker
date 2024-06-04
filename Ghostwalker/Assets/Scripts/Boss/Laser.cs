using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDistance;
    [SerializeField] private float damage;
    public Boss boss;

    private Vector3 initialPosition;
    private float moveTimer;

    private void Start()
    {
        initialPosition = transform.localPosition; 
    }

    public void RotateAroundBoss(Transform bossTransform)
    {
        transform.SetParent(bossTransform); 
    }

    private void Update()
    {
        if (boss.isDie)
        {
            Destroy(gameObject);
            return;
        }
        transform.RotateAround(transform.parent.position, Vector3.forward, rotateSpeed * Time.deltaTime);

        moveTimer += Time.deltaTime;
        var moveOffset = Mathf.Sin(moveTimer * moveSpeed) * moveDistance;
        transform.localPosition = initialPosition + transform.up * moveOffset;
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