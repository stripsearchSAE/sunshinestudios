using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    // FMOD.Studio.EventInstance femaleDenial = new FMOD.Studio.EventInstance[5]; // 3D female character sounds

    // Start is called before the first frame update
    void Start()
    {
        // string fn = "event:/Voices/Female/Denial" + (i + 1).ToString();
        // femaleDenial = FMODUnity.RuntimeManager.CreateInstance("event:/Voices/Female/Denial01");
        // femaleDenial.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
    }

    // Update is called once per frame
    void Update()
    {

    }

    // list of all the 3D sounds to be called
    public void PlayVoice(string clip)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Voices/" + clip, this.gameObject);
        //switch (clipToChoose) { case ("femaleDenial"): PlaySound3D(femaleDenial[Random.Range(0,femaleDenial.Length)]); break; }
    }

    /* private void PlaySound3D(FMOD.Studio.EventInstance clipToPlay)
    {
        // change to the following and do the necessary changes in the logic ... FMODUnity.RuntimeManager.PlayOneShotAttached("event:/" + clipToPlay, this.gameObject);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(clipToPlay, GetComponent<Transform>(), GetComponent<Rigidbody>());
        clipToPlay.start();
    } */
}