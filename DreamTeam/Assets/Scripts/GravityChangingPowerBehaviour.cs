using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangingPowerBehaviour : MonoBehaviour {
    //This script enables a player to change the Gravity
    private void Update() {
        if (Input.GetButtonDown("GravityChanger")) {
            PlayerGravityManagerBehaviour.instance.ChangeGravity();
        }
    }
}
