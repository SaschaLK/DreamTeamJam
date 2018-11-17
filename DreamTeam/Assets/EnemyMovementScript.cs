using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    private Transform Target; 

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.position, moveSpeed * Time.deltaTime);

    }

   
     void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
}