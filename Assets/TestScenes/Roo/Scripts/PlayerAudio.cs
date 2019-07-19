using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public static FMOD.Studio.EventInstance femaleDenial01; // 3D female character sounds

    // Start is called before the first frame update
    void Start()
    {
        femaleDenial01 = FMODUnity.RuntimeManager.CreateInstance("event:/Voices/Female/Denial01");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // list of all the 3D sounds to be called
    public void Playsound(string clip)
    {
        switch (clip) { case ("femaleDenial01"): femaleDenial01.start(); break; }
    }
}