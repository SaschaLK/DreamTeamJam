
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarateChop : MonoBehaviour
{

    private float timeBtwAttack;
    private float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public Animator canAnim;
    public Animator playerAnim;
    public float attackRangeX;
    public float attackRangeY;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0)
        {

            if (Input.GetKey(KeyCode.J))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
                //Debug.Log(enemiesToDamage.Length);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    Debug.Log("Hi");
                    //enemiesToDamage[i].GetComponent<Enemy>().Die();
                }

                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            startTimeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizomosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}