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
    public float randomYOffsetRange;
    public Vector3 triggerPlaneOffset;
    public GameObject renderTriggerPlane;

    private List<GameObject> floorTiles = new List<GameObject>();
    private bool chunk;
    private int tileAmountHalf;

    private void Awake() {
        instance = this;

        tileAmountHalf = tileAmount / 2;
    }

    private void Start() {
        //Setting Trigger Plane position on game start. One time only offset of 9.
        renderTriggerPlane.transform.position = new Vector3(triggerPlaneOffset.x - floorCeilingOffset.x / 2 + floorOffset.x + 9, 0, 0);
        renderTriggerPlane.SetActive(true);

        for(int i = 0; i < tileAmount; i++) {
            floorTiles.Add(Instantiate(floorPrefabTiles[Random.Range(0, floorPrefabTiles.Count - 1)], gameObject.transform));
            if (i != 0) {
                floorTiles[i].transform.position = floorTiles[i - 1].transform.position + floorCeilingOffset;
            }
        }
        foreach(GameObject floorTile in floorTiles) {
            floorTile.transform.position = floorTile.transform.position + floorOffset;
        }
        for(int i = 0; i < tileAmount; i++) {
            if (i != 0) {
                floorTiles[i].transform.position += new Vector3(0, Random.Range(-randomYOffsetRange, randomYOffsetRange) + (floorTiles[i-1].transform.position.y - floorTiles[i].transform.position.y), 0);
            }
        }

    }

    public void GenerateMapSegment() {
        if (!chunk) {
            //Repositioning of the first half of the Tiles
            chunk = !chunk;
            for (int i = 0; i < tileAmountHalf; i++) {
                if(i == 0) {
                    floorTiles[i].transform.position = new Vector3(floorTiles[i + tileAmountHalf].transform.position.x + triggerPlaneOffset.x, floorTiles[i + tileAmount - 1].transform.position.y + Random.Range(-randomYOffsetRange,randomYOffsetRange), 0);
                }
                else {
                    floorTiles[i].transform.position = new Vector3(floorTiles[i - 1].transform.position.x + floorCeilingOffset.x, floorTiles[i-1].transform.position.y + Random.Range(-randomYOffsetRange, randomYOffsetRange), 0);
                }
            }
        }
        else {
            //Repositioning of the second half of the Tiles
            chunk = !chunk;

            for(int i = tileAmountHalf; i < tileAmount; i++) {
                if(i == tileAmountHalf) {
                    floorTiles[i].transform.position = new Vector3(floorTiles[i + tileAmountHalf - 1].transform.position.x + triggerPlaneOffset.x + floorCeilingOffset.x, floorTiles[tileAmountHalf - 1].transform.position.y + Random.Range(-randomYOffsetRange, randomYOffsetRange), 0);
                }
                else {
                    floorTiles[i].transform.position = new Vector3(floorTiles[i - 1].transform.position.x + floorCeilingOffset.x, floorTiles[i - 1].transform.position.y + Random.Range(-randomYOffsetRange, randomYOffsetRange), 0);
                }
            }
        }
    }
}
