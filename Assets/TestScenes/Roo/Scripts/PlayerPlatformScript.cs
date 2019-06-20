﻿/********************************************************************
 *  Hey team. This script will move the player platform perfectly   *
 *  to the center of the action. All you need to do is drag the     *
 *  lava object and erruption viewing point into the inspector      *
 *  the script will do the rest. there is an offset value if you    *
 *  dont like where the platform sits. there is also dampening      *
 *  tweaks available.                                               *
 *                                                                  *
 *  Right now the end sequence is set to the space bar for testing  *
 *  this code will be swapped out to an event call when the game    *
 *  is near completion...                                           *
 *                             ENJOY :)                             *
 ********************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformScript : MonoBehaviour
{
    public GameObject[] Explorers;
    public float platformDampening = 5f;
    public float platformDampeningErruption = 3.5f;
    [Range(-10f, 10f)] public float platformOffset = 0f;
    public Transform Lava; // drag lava gameobject into inspector
    public Transform erruptionViewPoint;
    

    private bool _erruptionHappening = false;

    // Start is called before the first frame update
    void Start()
    {
        Explorers = GameObject.FindGameObjectsWithTag("Explorer"); // find all explorers in the scene
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) _erruptionHappening = true; // will swap this out for event call later

        if (_erruptionHappening)
        {
            transform.position = Vector3.Lerp(transform.position, erruptionViewPoint.position, Time.deltaTime / platformDampeningErruption);
        }
        else
        {
            float _averageYposition = Lava.position.y;
            foreach (GameObject explorer in Explorers)
            {
                _averageYposition = _averageYposition + CheckExplorerPosition(explorer.transform.position.y); // checks to see if player is below lava line.. return lava position if this is true
            }

            _averageYposition = (_averageYposition / (Explorers.Length + 1)) + platformOffset; // calculates final average y position
            Debug.Log(_averageYposition);

            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, _averageYposition, transform.position.z), Time.deltaTime / platformDampening); // smooth lerp to average position
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, Lava.position.y, 1000f), transform.position.z); // clamp position to not go under lava position
        }

    }


    // if the explorer falls into the lava, height will return the lava height
    private float CheckExplorerPosition(float explorerPos)
    {
        if (explorerPos < Lava.position.y) return Lava.position.y;
        return explorerPos;
    }
}