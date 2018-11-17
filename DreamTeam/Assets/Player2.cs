using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player2 : MonoBehaviour
{

    // Config 
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidBody2;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet2;
    float gravityScaleAtStart;

    // Message then methods
    void Start()
    {
        myRigidBody2 = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet2 = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody2.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        //ClimbLadder();
        Jump();
        Gravity();
        FlipSprite();
        Die();
    }

    private void Gravity() {
        if (Input.GetKeyDown("t") || Input.GetKeyDown("w"))
        {
            myRigidBody2.gravityScale = myRigidBody2.gravityScale * -1;
            //Player2.
            // myRigidBody2.gravityScale = 0f;

        }
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal2"); // value is betweeen -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody2.velocity.y);
        myRigidBody2.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody2.velocity.x) > Mathf.Epsilon;
        //myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void ClimbLadder()
    {
        if (!myFeet2.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            //myAnimator.SetBool("Climbing", false);
            myRigidBody2.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody2.velocity.x, controlThrow * climbSpeed);
        myRigidBody2.velocity = climbVelocity;
        myRigidBody2.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody2.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);

    }

    private void Jump()
    {
        
        if (!myFeet2.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (Input.GetKeyDown("up"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody2.velocity += jumpVelocityToAdd;
        }
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
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