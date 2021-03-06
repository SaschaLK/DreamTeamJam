﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Flipper : MonoBehaviour {
    
    public RectTransform startPos, endPos;
    public float speed = 1.0f;

    private float startTime, totalDistance;
    private RectTransform rect;
    private float journeyFraction;
    private bool flip = false;
    private bool flipped = false; 

    IEnumerator Start()
    {
        rect = this.GetComponent<RectTransform>();

        totalDistance = Vector3.Distance(startPos.anchoredPosition, endPos.anchoredPosition);

        yield return null; 
    }

    private void Update()
    {
        if(flip)
        {
            flip = false;
            StartCoroutine("Move"); 
        }
    }

    public IEnumerator Move()
    {
        startTime = Time.time;

        Debug.Log("move"); 
        Debug.Log(journeyFraction < 0.5f);
        Debug.Log("Move"); 
        while (journeyFraction < 0.8f)
        {
            Debug.Log(journeyFraction); 
            float currentDuration = (Time.time - startTime) * speed;
            journeyFraction = currentDuration / totalDistance;
            if(flipped)
            {
                rect.anchoredPosition = Vector3.Lerp(endPos.anchoredPosition, startPos.anchoredPosition, journeyFraction);
            } else
            {
                rect.anchoredPosition = Vector3.Lerp(startPos.anchoredPosition, endPos.anchoredPosition, journeyFraction);
            } 

            yield return new WaitForEndOfFrame(); 
        }

        if (flipped)
        {
            rect.anchoredPosition = startPos.anchoredPosition; 
            totalDistance = Vector3.Distance(endPos.anchoredPosition, startPos.anchoredPosition);
        } else
        {
            rect.anchoredPosition = endPos.anchoredPosition;  
            totalDistance = Vector3.Distance(startPos.anchoredPosition, endPos.anchoredPosition);
        }
        journeyFraction = 1f;

        flipped = !flipped;
    }
    
    public void Flip()
    {
        flip = true; 
    }

}
