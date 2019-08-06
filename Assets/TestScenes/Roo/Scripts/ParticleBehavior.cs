using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehavior : MonoBehaviour
{
    public GameObject Smoke;
    public GameObject LavaErrupt;

    public float timeToWait;
    // Start is called before the first frame update
    void Start()
    {
        ExplorerEndSequence.StartFinalSequence += VolcanoSequence;
    }

    void VolcanoSequence()
    {
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        Smoke.SetActive(true);
        yield return new WaitForSeconds(timeToWait);

        LavaErrupt.SetActive(true);
    }
}
