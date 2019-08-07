using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ExplorerEndSequence.StartFinalSequence += PlayEruptionSound;
    }

    void PlayEruptionSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Eruption01", this.gameObject);
    }

}
