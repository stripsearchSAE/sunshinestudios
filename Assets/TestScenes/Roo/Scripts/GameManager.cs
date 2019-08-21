using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CanvasGroup blackoutCanvas;

    public float timeToWaitForReset;
    public float fadeInTime;
    [Header("use Music time for global fadeout")]
    [Range(3.01f, 30f)] public float timeForMusicFadeout = 3.01f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Deactivate(blackoutCanvas, fadeInTime));
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
        StartCoroutine(Activate(blackoutCanvas, timeForMusicFadeout));
        yield return new WaitForSeconds(timeForMusicFadeout);
        BaseAudioManager.Playsound("stopOcean");
        BaseAudioManager.Playsound("startVolcano");
        BaseAudioManager.Playsound("Level1");
        BaseAudioManager.Playsound("startMusic");
        

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Deactivate(CanvasGroup _panel, float _transitionTime)
    {
        while (_panel.alpha > 0)
        {
            _panel.alpha -= Time.deltaTime / _transitionTime;
            yield return null;
        }
        _panel.interactable = false;
        _panel.blocksRaycasts = false;

        yield return null;
    }

    IEnumerator Activate(CanvasGroup _panel, float _transitionTime)
    {
        while (_panel.alpha < 1)
        {
            _panel.alpha += Time.deltaTime / _transitionTime;
            yield return null;
        }
        _panel.interactable = true;
        _panel.blocksRaycasts = true;

        yield return null;
    }
}
