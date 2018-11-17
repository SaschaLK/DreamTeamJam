using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player2 : MonoBehaviour
{
    public static Player2 instance;
    // Config 
    [SerializeField] float runSpeed = 5f;
    public float jumpSpeed = 5f;

    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidBody2;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;

    private void Awake() {
        instance = this;
        gameObject.SetActive(true);
    }

    // Message then methods
    void Start()
    {
        myRigidBody2 = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Jump();
        FlipSprite();
        Die();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal_Player2"); // value is betweeen -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody2.velocity.y);
        myRigidBody2.velocity = playerVelocity;
    }

    private void Jump()
    {
        
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (Input.GetButtonDown("Vertical_Player2"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody2.velocity += jumpVelocityToAdd;
        }
    }

    public void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            //isAlive = false;
            //myAnimator.SetTrigger("Dying");
            //GetComponent<Rigidbody2D>().velocity = deathKick;
            gameObject.SetActive(false);
        }
    }

    private void FlipSprite()
    {
        float scaleScalar = Mathf.Abs(transform.localScale.x);
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody2.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector3(Mathf.Sign(myRigidBody2.velocity.x), 1f, 1f) * scaleScalar;
        }
    }

}