using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class cameraDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        Player2.instance.Die();
        Player.instance.Die();
    }
}