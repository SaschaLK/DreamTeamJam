using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorBehaviour : MonoBehaviour {

    public static MapGeneratorBehaviour instance;

    public int tileAmount;
    public int renderedTileAmount;
    public List<GameObject> floorPrefabTiles = new List<GameObject>();
    public List<GameObject> ceilingPrefabTiles = new List<GameObject>();
    public Vector3 floorCeilingOffset;
    public Vector3 floorOffset;
    public Vector3 triggerPlaneOffset;
    public GameObject renderTriggerPlane;

    private List<GameObject> floorTiles = new List<GameObject>();

    private void Awake() {
        instance = this;
    }

    private void Start() {
        renderTriggerPlane.transform.position = triggerPlaneOffset;
        renderTriggerPlane.SetActive(true);

        for(int i = 0; i < tileAmount; i++) {
            floorTiles.Add(Instantiate(floorPrefabTiles[Random.Range(0, floorPrefabTiles.Count - 1)], gameObject.transform));
            if (i != 0) {
                floorTiles[i].transform.position = floorTiles[i - 1].transform.position + floorCeilingOffset;
            }
            //floorTiles[i].transform.position.Set(floorTiles[i].transform.position.x, floorOffset.y, floorTiles[i].transform.position.z);
        }
        foreach(GameObject floorTile in floorTiles) {
            floorTile.transform.position = floorTile.transform.position + floorOffset;
        }

    }

    public void GenerateMapSegment() {
    }
}
