using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2((Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0),
            (Input.GetKey(KeyCode.S) ? -1 : 0) + (Input.GetKey(KeyCode.W) ? 1 : 0));
        rb.velocity = movement.normalized * moveSpeed;
        
        animator.SetBool("isRunning", !movement.Equals(Vector2.zero));
        if (Math.Abs(movement.x) > 1e-8)
            spriteRenderer.flipX = movement.x < 0;
    }
}
