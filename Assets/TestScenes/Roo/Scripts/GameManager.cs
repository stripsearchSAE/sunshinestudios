using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float timeToWaitForReset;
    public float timeforMusicFadeout = 3.01f;
    // Start is called before the first frame update
    void Start()
    {
        ExplorerEndSequence.StartFinalSequence += StartCountdown;
    }

    void StartCountdown()
    {
        StartCoroutine(ResetScene());
    }

    IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(timeToWaitForReset);
        BaseAudioManager.Playsound("stopMusic");
        yield return new WaitForSeconds(timeforMusicFadeout);
        BaseAudioManager.Playsound("stopOcean");
        BaseAudioManager.Playsound("startVolcano");
        BaseAudioManager.Playsound("Level1");
        BaseAudioManager.Playsound("startMusic");
        

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
