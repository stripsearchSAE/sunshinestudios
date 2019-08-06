/********************************************************************
 *  Hey team. This script will move the player platform perfectly   *
 *  to the center of the action. All you need to do is tag the      *
 *  explorers with the tag "Explorers" (important),   drag the      *
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
    public enum States {ELEVATOR, ERRUPTION};
    public States PlatformState;

    public float WaitTime;
    public bool Errupting;
    public GameObject Smoke;
    public GameObject LavaErrupt;
    public GameObject[] Explorers;
    public float platformDampening = 5f;
    public float platformDampeningErruption = 3.5f;
    public bool calcLavaPosition = true;
    [Range(-10f, 10f)] public float platformOffset = 0f;
    public Transform Lava; // drag lava gameobject into inspector
    public Transform erruptionViewPoint; // point at which the platform will lerp to at end of game

    private Vector3 velocity = Vector3.zero; // refernce for smoothdamp

    private bool hasCalledSound = false;

    // private bool _erruptionHappening = false;

    // Start is called before the first frame update
    void Start()
    {
        ExplorerEndSequence.StartFinalSequence += SwitchStates;
        PlatformState = States.ELEVATOR;
        Explorers = GameObject.FindGameObjectsWithTag("Explorer"); // find all explorers in the scene
    }

    private void Update()
    {
        if(Errupting == true)
        {
            WaitTime = WaitTime + Time.deltaTime * 1;
        }
        if (WaitTime > 9.5f)
        {
            LavaErrupt.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space)) PlatformState = States.ERRUPTION; // will swap this out for event call later
        //if (transform.position.y >= 4f) PlatformState = States.ERRUPTION; // for testing
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (PlatformState)
        {
            case States.ELEVATOR:
                float _averageYposition = 0f;
                int _divider = Explorers.Length;
                if (calcLavaPosition)
                {
                    _averageYposition = Lava.position.y;
                    _divider += 1;
                }
                foreach (GameObject explorer in Explorers)
                {
                    _averageYposition = _averageYposition + CheckExplorerPosition(explorer.transform.position.y); // checks to see if player is below lava line.. return lava position if this is true
                }

                _averageYposition = (_averageYposition / _divider) + platformOffset; // calculates final average y position
                // Debug.Log(_averageYposition);

                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, _averageYposition, transform.position.z), ref velocity, platformDampening);
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, Lava.position.y, 1000f), transform.position.z); // clamp position to not go under lava position
                break;

            case States.ERRUPTION:

                Smoke.SetActive(true);
                Errupting = true;

                if (!hasCalledSound)
                {
                    hasCalledSound = true;
                    BaseAudioManager.Playsound("startOcean");
                    BaseAudioManager.Playsound("stopVolcano");
                    FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Eruption01", this.gameObject); // this is to test timing.. move script to appropriate place after
                }

                transform.position = Vector3.SmoothDamp(transform.position, erruptionViewPoint.transform.position, ref velocity, platformDampeningErruption);
                

                break;
        }

    }

    void SwitchStates()
    {
        PlatformState = States.ERRUPTION;
    }
    // if the explorer falls into the lava, height will return the lava height
    private float CheckExplorerPosition(float explorerPos)
    {
        if (explorerPos < Lava.position.y) return Lava.position.y;
        return explorerPos;
    }
}
