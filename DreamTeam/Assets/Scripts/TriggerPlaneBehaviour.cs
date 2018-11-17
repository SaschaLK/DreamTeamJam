using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlaneBehaviour : MonoBehaviour {
    //Script for Triggering the new Render Sequenz for the next Segment
    private void OnTriggerEnter2D(Collider2D collision) {
        MapGeneratorBehaviour.instance.GenerateMapSegment();
        gameObject.transform.position = gameObject.transform.position + MapGeneratorBehaviour.instance.triggerPlaneOffset;
    }
}
