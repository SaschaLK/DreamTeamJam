using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {

    public GameObject mob;

    private void Start() {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() {
        while (true) {
            yield return new WaitForSeconds(5f);
            Instantiate(mob);
        }
    }
}
