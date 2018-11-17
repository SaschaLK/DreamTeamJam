using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTestPlayerBehaviour : MonoBehaviour {
    //THIS WHOLE SCRIPT IS FOR TEST PURPOSES ONLY
    
    private void OnCollisionEnter2D(Collision2D collision) {
        PlayerGravityManagerBehaviour.instance.ChangeGravity();
    }
}
