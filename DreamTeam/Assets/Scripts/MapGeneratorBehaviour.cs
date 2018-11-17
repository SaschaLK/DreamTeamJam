using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorBehaviour : MonoBehaviour {

    public static MapGeneratorBehaviour instance;

    public int tileAmount;
    public int renderedTileAmount;
    public List<GameObject> floorPrefabTiles = new List<GameObject>();
    public List<GameObject> ceilingPrefabTiles = new List<GameObject>();
    public Vector3 SingleTileOffset;
    public Vector3 startTileOffset;
    public float randomYOffsetRange;
    public Vector3 triggerPlaneOffset;
    public GameObject renderTriggerPlane;

    private List<GameObject> ceilingTiles = new List<GameObject>();
    private List<GameObject> floorTiles = new List<GameObject>();
    private bool chunk;
    private int tileAmountHalf;

    private void Awake() {
        instance = this;
        tileAmountHalf = tileAmount / 2;
    }

    private void Start() {
        //Setting Trigger Plane position on game start. One time only offset of 9.
        renderTriggerPlane.transform.position = new Vector3(triggerPlaneOffset.x - SingleTileOffset.x / 2 + startTileOffset.x + 9, 0, 0);
        renderTriggerPlane.SetActive(true);

        //ÌNSTANTIATE AND SETUP OF FLOOR AND CEILING
        InstantiateSetup(ceilingTiles, ceilingPrefabTiles, startTileOffset.x, -startTileOffset.y);
        InstantiateSetup(floorTiles, floorPrefabTiles, startTileOffset.x, startTileOffset.y);
    }

    private void InstantiateSetup(List<GameObject> tiles, List<GameObject> prefabTiles, float rowStartX, float rowStartY) {
        //Instatiate Tiles and set in Row
        for (int i = 0; i < tileAmount; i++) {
            tiles.Add(Instantiate(prefabTiles[Random.Range(0, prefabTiles.Count - 1)], gameObject.transform));
            if (i != 0) {
                tiles[i].transform.position = tiles[i - 1].transform.position + SingleTileOffset;
            }
        }
        //Offset Tilerow into Position
        foreach (GameObject tile in tiles) {
            tile.transform.position = tile.transform.position + new Vector3(rowStartX, rowStartY, 0);
        }
        //Waveform / Disturbance in Row
        for (int i = 0; i < tileAmount; i++) {
            if (i != 0) {
                tiles[i].transform.position += new Vector3(0, Random.Range(-randomYOffsetRange, randomYOffsetRange) + (tiles[i - 1].transform.position.y - tiles[i].transform.position.y), 0);
            }
        }
    }

    public void GenerateMapSegment() {
        if (!chunk) {
            //Repositioning of the first half of the Tiles
            chunk = !chunk;
            for (int i = 0; i < tileAmountHalf; i++) {
                if(i == 0) {
                    ceilingTiles[i].transform.position = new Vector3(ceilingTiles[tileAmountHalf].transform.position.x + triggerPlaneOffset.x, ceilingTiles[i + tileAmount - 1].transform.position.y + Random.Range(-randomYOffsetRange,randomYOffsetRange), 0);
                    floorTiles[i].transform.position = new Vector3(floorTiles[tileAmountHalf].transform.position.x + triggerPlaneOffset.x, floorTiles[i + tileAmount - 1].transform.position.y + Random.Range(-randomYOffsetRange, randomYOffsetRange), 0);
                }
                else {
                    LoadChunkRest(i);
                }
            }
        }
        else {
            //Repositioning of the second half of the Tiles
            chunk = !chunk;
            for(int i = tileAmountHalf; i < tileAmount; i++) {
                if(i == tileAmountHalf) {
                    ceilingTiles[i].transform.position = new Vector3(ceilingTiles[i + tileAmountHalf - 1].transform.position.x + triggerPlaneOffset.x + SingleTileOffset.x, ceilingTiles[tileAmountHalf - 1].transform.position.y + Random.Range(-randomYOffsetRange, randomYOffsetRange), 0);
                    floorTiles[i].transform.position = new Vector3(floorTiles[i + tileAmountHalf - 1].transform.position.x + triggerPlaneOffset.x + SingleTileOffset.x, floorTiles[tileAmountHalf - 1].transform.position.y + Random.Range(-randomYOffsetRange, randomYOffsetRange), 0);
                }
                else {
                    LoadChunkRest(i);
                }
            }
        }
    }
    //TO DO: CLEAN UP / SIMPLIFICATION OF CODE ABOVE. DRY
    private void LoadChunkRest(int i) {
        ceilingTiles[i].transform.position = new Vector3(ceilingTiles[i - 1].transform.position.x + SingleTileOffset.x, ceilingTiles[i - 1].transform.position.y + Random.Range(-randomYOffsetRange, randomYOffsetRange), 0);
        floorTiles[i].transform.position = new Vector3(floorTiles[i - 1].transform.position.x + SingleTileOffset.x, floorTiles[i - 1].transform.position.y + Random.Range(-randomYOffsetRange, randomYOffsetRange), 0);
    }
}
