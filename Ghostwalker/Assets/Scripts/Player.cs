using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private bool facingRight = true;
    private Vector2 movement;

    private Animator animator;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement = new Vector2(
            (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0),
            (Input.GetKey(KeyCode.S) ? -1 : 0) + (Input.GetKey(KeyCode.W) ? 1 : 0));
        
        animator.SetBool("isRunning", !movement.Equals(Vector2.zero));

        if (facingRight && movement.x < -1e-8 || !facingRight && movement.x > 1e-8)
            Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = movement.normalized * moveSpeed;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        var scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
