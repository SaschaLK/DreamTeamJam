using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorBehaviour : MonoBehaviour {

    public static MapGeneratorBehaviour instance;

    public int tileAmount;
    public List<GameObject> floorTiles = new List<GameObject>();
    public List<GameObject> ceilingTiles = new List<GameObject>();
    public Vector3 floorCeilingOffset;
    public Vector3 triggerPlaneOffset;
    public GameObject renderTriggerPlane;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        renderTriggerPlane.transform.position = triggerPlaneOffset;
        renderTriggerPlane.SetActive(true);
    }

    public void GenerateMapSegment() {
        Debug.Log("Hello");
    }
}
