using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



/*
 TODO
 ∆ 1. test btn onclick listener (sth wrong w/ playbtn)
 √ 1. change vars to audiomanager's vars
 2. add clear game
 3. delete file in ios sandbox
*/



public class Game : MonoBehaviour
{
    public Button loadBtn;
    public Button saveBtn;

    public AudioManager audioManager;
    AudioMixer mixer;
    Slider pitchSlider;
    Slider flangeSlider;

    /*
    public Saver saver;
    */

    void Start()
    {
        //mixer = GameObject.Find("AudioCtrl").GetComponent<AudioManager>().mixer;
        //flangeSlider = GameObject.Find("AudioCtrl").GetComponent<AudioManager>().flangeSlider;
        //pitchSlider = GameObject.Find("AudioCtrl").GetComponent<AudioManager>().pitchSlider;
        mixer = audioManager.mixer;
        flangeSlider = audioManager.flangeSlider;
        pitchSlider = audioManager.pitchSlider;

        /*
        //create saver object
        saver = CreateSaverGameObject();
        */
    }

    private void OnEnable()
    {
        loadBtn.onClick.AddListener(OnLoadBtnClicked);
        saveBtn.onClick.AddListener(OnSaveBtnClicked);
    }

    private void OnDisable()
    {
        loadBtn.onClick.RemoveListener(OnLoadBtnClicked);
        saveBtn.onClick.RemoveListener(OnSaveBtnClicked);
    }

    private void OnLoadBtnClicked()
    {
        LoadGame();
    }

    private void OnSaveBtnClicked()
    {
        SaveGame();
    }

    private Saver CreateSaverGameObject()
    {
        Saver saver = new Saver();
        bool hasFlangeDepth = mixer.GetFloat("flange_depth", out float flangeDepthValue);
        if (hasFlangeDepth)
        {
            saver.saverFlangeDepth = flangeDepthValue;
        }
        bool hasPitch = mixer.GetFloat("pitch", out float pitchValue);
        if (hasPitch)
        {
            saver.saverPitch = pitchValue;
        }
        return saver;
    }

    /* dict create saver
    private Saver CreateSaverGameObject()
    {
        Saver saver = new Saver();
        saver.saverTimeSamples.Add(0.0f);
        saver.saverReadings.Add(new Dictionary<string, float> { { "pitch", 1.0f }, { "flange_depth", 0.0f }, { "flange_rate", 0.0f } });
        return saver;
    }
    */

    public void SaveGame()
    {
        Saver saver = CreateSaverGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, saver);
        file.Close();
        Debug.Log("[Game] Game saved.");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Saver saver = (Saver)bf.Deserialize(file);
            //saver = (Saver)bf.Deserialize(file);
            file.Close();

            #region dict save
            /* 
            // "init" the lists
            // not really init, bc the init should be on awake (in audiomanager script)
            audioManager.manipulationsTimeSamples = saver.saverTimeSamples;
            audioManager.manipulationsReadings = saver.saverReadings;

            if (saver.saverTimeSamples.Count > 1)
            {
                // disable recreation
                audioManager.pitchToggle.interactable = false;
                audioManager.pitchEnvToggle.interactable = false;
                audioManager.pitchSlider.interactable = false;
                audioManager.flangeToggle.interactable = false;
                audioManager.flangeEnvToggle.interactable = false;
                audioManager.flangeSlider.interactable = false;

                audioManager.pitchToggle.isOn = false;
                audioManager.pitchEnvToggle.isOn = false;
                audioManager.flangeToggle.isOn = false;
                audioManager.flangeEnvToggle.isOn = false;

                audioManager.index = 0;
                audioManager.isPreloaded = true;
            }
            else
            {
                audioManager.lastReadings = saver.saverReadings[0];
                audioManager.isPreloaded = false;
            }
            */
            #endregion

            mixer.SetFloat("flange_depth", saver.saverFlangeDepth);
            flangeSlider.value = saver.saverFlangeDepth;
            mixer.SetFloat("pitch", saver.saverPitch);
            pitchSlider.value = saver.saverPitch;

            // debug
            bool hasFlangeDepth = mixer.GetFloat("flange_depth", out float flangeDepthValue);
            if (hasFlangeDepth)
            {
                Debug.LogFormat("[Game] loaded flange_depth = {0}", flangeDepthValue);
            }
            Debug.LogFormat("[Game] Game loaded.");

        } else
        {
            Debug.Log("[Game] No game saved!");
        }
    
    }

    public void ClearGame()
    {

    }

}
