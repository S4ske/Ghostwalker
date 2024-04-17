using System;
using UnityEngine;

public class Idle : MonoBehaviour
{
    public float moveSpeed;

    private Vector2 moveInput;
    private Vector2 moveVelocity;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Rigidbody2D theRB;
    
    
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;

        theRB.velocity = moveVelocity;
        
        animator.SetBool("isRunning", !moveInput.Equals(Vector2.zero));
        if (Math.Abs(moveInput.x) >= 1e-8)
            spriteRenderer.flipX = moveInput.x < 0;
    }
}
