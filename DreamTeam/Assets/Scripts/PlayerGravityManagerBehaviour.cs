using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityManagerBehaviour : MonoBehaviour {

    public static PlayerGravityManagerBehaviour instance;

    public List<GameObject> players = new List<GameObject>();
    private List<Rigidbody2D> playerRigidbodies = new List<Rigidbody2D>();

    private void Awake() {
        instance = this;
    }

    private void Start() {
        foreach(GameObject player in players) {
            player.SetActive(true);
            playerRigidbodies.Add(player.GetComponent<Rigidbody2D>());
        }
    }

    // Public Function to change the Gravity for all Players
    public void ChangeGravity() {
        foreach(Rigidbody2D playerRigidbody in playerRigidbodies) {
            playerRigidbody.gravityScale = playerRigidbody.gravityScale * -1;
        }
        foreach(GameObject player in players) {
            player.GetComponent<SpriteRenderer>().flipY = !player.GetComponent<SpriteRenderer>().flipY;
        }

    }
}
