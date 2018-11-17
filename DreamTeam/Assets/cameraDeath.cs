using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class cameraDeath : MonoBehaviour
{

    private int speed = 1;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }
}