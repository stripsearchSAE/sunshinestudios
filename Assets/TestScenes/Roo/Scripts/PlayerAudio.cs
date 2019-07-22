using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public FMOD.Studio.EventInstance femaleDenial; // 3D female character sounds

    // Start is called before the first frame update
    void Start()
    {
        femaleDenial = FMODUnity.RuntimeManager.CreateInstance("event:/Voices/Female/Denial01");
        //femaleDenial[1] = FMODUnity.RuntimeManager.CreateInstance("event:/Voices/Female/Denial02");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // list of all the 3D sounds to be called
    public void Playsound(string clip)
    {
        switch (clip) { case ("femaleDenial"): femaleDenial.start(); break; }
        // switch (clip) { case ("femaleDenial"): femaleDenial[Random.Range(0,femaleDenial.Length)].start(); break; }
    }
}