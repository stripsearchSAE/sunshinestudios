﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAudioManager : MonoBehaviour
{
    /***************************************************************************************************
     *                          -= !!! ATTENTION CODERS OF LAVE ESCAPE !!!=-                            *     
     *                                                                                                  *
     *       with this script loaded, all you need to do to play a sound is to use the code below...    *
     *   you can call this from any script on any object without requiring to reference anything else   *
     *                                                                                                  *
     *                      AudioMasterScript.Playsound("NAME OF SOUND HERE");                          *
     *                                                                                                  *
     *                      just follow the code below to add the sounds from hte bank                  *
     *                      first list the sounds,                                                      *
     *                      then connect to the sounds in the bank                                      *
     *                      then add to the PlaySound() function                                        *
     *                                                                                                  *
     *                                                                                                  *
     *                                                                                                  *
     *                                       Enjoy, Roo :)                                              *
     *                                                                                                  *
     ***************************************************************************************************/

   
    public static FMOD.Studio.EventInstance volcanoAtmos, oceanAtmos; // atmos variables
    public static FMOD.Studio.EventInstance score;

    public static FMOD.Studio.ParameterInstance musicLevel; // parameter to control reactive music

    // below is examples of links to parameters inside of an eventinstance
    // name the parameters for wind variables
    // public static FMOD.Studio.ParameterInstance WindIntensity; // name the parameter for wind intensity
    // public static FMOD.Studio.ParameterInstance WindVol; // name the parameter for wind Volume


    // Keep music rolling between scenes. Will not destroy onload unless there is a double.
    static BaseAudioManager instance = null;

   void Awake()
   {
       if (instance != null)
       {
           Destroy(gameObject);
       }
       else
       {
           instance = this;
           GameObject.DontDestroyOnLoad(gameObject);
       }
   }

   // Start is called before the first frame update
   void Start()
   {
       // below we connect to all sounds in FMOD for the game
       // atmos bank
       volcanoAtmos = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Burning");
       oceanAtmos = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Ocean");

       // music
       score = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Score");


        // connect to sound parameters
       score.getParameter("GameLevel", out musicLevel);

       score.start();
       volcanoAtmos.start(); // start the volcano atmos
       // atmosExploring.start(); // start exploring atmos
   }

   // Below is the switch statement for all the possible sounds used in the game
   public static void Playsound(string clip)
   {
        switch (clip) { case ("startOcean"): oceanAtmos.start(); break; }
        switch (clip) { case ("stopOcean"): oceanAtmos.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); break; }
        switch (clip) { case ("startVolcano"): volcanoAtmos.start(); break; }
        switch (clip) { case ("stopVolcano"): volcanoAtmos.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); break; }
        switch (clip) { case ("startMusic"): score.start(); break; }
        switch (clip) { case ("stopMusic"): score.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); break; }
        switch (clip) { case ("Level1"): musicLevel.setValue(1f); break; }
        switch (clip) { case ("Level10"): musicLevel.setValue(10f); break; }
    }
   
}
