using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangingPowerBehaviour : MonoBehaviour {
    //This script enables a player to change the Gravity
    public float gravityChangingPowerCooldown = 1.5f;
    private bool gravityChangingInProcess;

    private void Update() {
        if (!gravityChangingInProcess && Input.GetButtonDown("GravityChanger")) {
            PlayerGravityManagerBehaviour.instance.ChangeGravity();
            StartCoroutine(GravityChanging());
        }
    }

    IEnumerator GravityChanging() {
        gravityChangingInProcess = true;
        Player.instance.jumpSpeed *= -1;
        Player2.instance.jumpSpeed *= -1;

        yield return new WaitForSeconds(gravityChangingPowerCooldown);
        gravityChangingInProcess = false;
    }
}
