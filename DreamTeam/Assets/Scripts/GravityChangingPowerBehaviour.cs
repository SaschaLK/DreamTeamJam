using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangingPowerBehaviour : MonoBehaviour {
    //This script enables a player to change the Gravity
    public float gravityChangingPowerCooldown;
    private bool gravityChangingInProcess;

    private void Update() {
        if (!gravityChangingInProcess && Input.GetButtonDown("GravityChanger")) {
            PlayerGravityManagerBehaviour.instance.ChangeGravity();
            StartCoroutine(GravityChanging());
        }
    }

    IEnumerator GravityChanging() {
        gravityChangingInProcess = true;
        yield return new WaitForSeconds(gravityChangingPowerCooldown);
        gravityChangingInProcess = false;
    }
}
