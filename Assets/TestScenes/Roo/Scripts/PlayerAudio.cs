using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio is Male by Default")]
    public bool isFemale = false;
    private string gender = "Male";

    // Start is called before the first frame update
    void Start()
    {
        if (isFemale) gender = "Female";
    }

    // Update is called once per frame
    void Update()
    {

    }

    // list of all the 3D sounds to be called
    public void PlayVoice(string clip)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Voices/" + gender + "/" + clip, this.gameObject);
    }
}