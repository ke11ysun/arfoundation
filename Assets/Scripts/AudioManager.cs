using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


/*
 TODO
 1. set each slider with sensible min/max, init with appropriate values
 2. confirm timesample format
*/



public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;

    public Toggle pitchToggle;
    public Toggle pitchEnvToggle;
    public Slider pitchSlider;

    public Toggle flangeToggle;
    public Toggle flangeEnvToggle;
    public Slider flangeSlider;

    /* dict save params
    //dict of dict for storing manipulations, only storing delta
    public AudioSource audioSource;
    private static string[] parameters = { "pitch", "flange_depth", "flange_rate" };
    public Dictionary<string, float> lastReadings = new Dictionary<string, float> { { "pitch", 1.0f }, { "flange_depth", 0.0f }, {"flange_rate", 0.0f } };
    public List<float> manipulationsTimeSamples;
    public List<Dictionary<string, float>> manipulationsReadings; // equivalence of the dict; the two should have same length

    public Game game;
    public int index = -1;
    public bool isPreloaded = false;
    */


    private void Awake()
    {
        //pitchToggle = GetComponent<Toggle>();

        /*
        // init manipulation dict with readings at 00:00:00 aka all default values
        audioSource = GetComponent<AudioSource>();
        //manipulationsTimeSamples = game.GetComponent<Saver>().saverTimeSamples;
        //manipulationsReadings = game.GetComponent<Saver>().saverReadings;
        //lastReadings = manipulationsReadings[0]; // is always default values at 00:00:00
        manipulationsTimeSamples.Add(0.0f);
        manipulationsReadings.Add(lastReadings);
        */
        
    }

    private void OnEnable()
    {
        pitchToggle.onValueChanged.AddListener(OnPitchToggleValueChanged);
        flangeToggle.onValueChanged.AddListener(OnFlangeToggleValueChanged);
    }

    private void OnDisable()
    {
        pitchToggle.onValueChanged.RemoveListener(OnPitchToggleValueChanged);
        flangeToggle.onValueChanged.RemoveListener(OnFlangeToggleValueChanged);
    }

    private void OnPitchToggleValueChanged(bool value)
    {
        Debug.LogFormat("[PitchToggle] {0}", value);
        if (value)
        {
            Debug.LogFormat("PitchSlider value: {0}", pitchSlider.value);
            mixer.SetFloat("pitch", pitchSlider.value);
        } else
        {
            mixer.SetFloat("pitch", 1.0f);
            pitchSlider.value = 1.0f;
            Debug.Log("PitchSlider is disabled!");
        }
    }

    private void OnFlangeToggleValueChanged(bool value)
    {
        Debug.LogFormat("[FlangeToggle] {0}", value);
        if (value)
        {
            Debug.LogFormat("FlangeSlider value: {0}", flangeSlider.value);
            mixer.SetFloat("flange_depth", flangeSlider.value);
        }
        else
        {
            mixer.SetFloat("flange_depth", 0.0f);
            //mixer.SetFloat("flange_rate", 0.0f);
            flangeSlider.value = 0.0f;
            Debug.Log("FlangeSlider is disabled!");
        }
    }

    private void Update()
    {
        // isOn means not preloaded; game script on load turns off all toggles
        if (pitchToggle.isOn)
        {
            if (!pitchEnvToggle.isOn)
            {
                mixer.SetFloat("pitch", pitchSlider.value);
            } else
            {
                mixer.SetFloat("pitch", Input.acceleration.x);
            }
            
        }

        if (flangeToggle.isOn)
        {
            mixer.SetFloat("flange_depth", flangeSlider.value);
            mixer.SetFloat("flange_rate", 1.5f);
        }



        #region dict save
        /*
        if (manipulationsReadings == null || manipulationsTimeSamples == null || manipulationsTimeSamples.Count != manipulationsReadings.Count)
        {
            Debug.Log("manipulation dict not correctly initialized!");
        }

        float currentTimeSample = audioSource.timeSamples;
        Dictionary<string, float> currentReadings = new Dictionary<string, float>();

        if (!isPreloaded)
        {
            // if not preloaded: save current manipulations
            foreach (KeyValuePair<string, float> entry in lastReadings)
            {
                string key = entry.Key;
                float lastValue = entry.Value;
                mixer.GetFloat(key, out float currentValue);
                if (currentValue != lastValue)
                {
                    currentReadings[key] = currentValue;
                    lastReadings[key] = currentValue; // lastReadings always has all params, keeps latest readings, might not obtained at same timesample
                }
            }
            if (currentReadings.Count != 0)
            {
                manipulationsTimeSamples.Add(currentTimeSample);
                manipulationsReadings.Add(currentReadings);
            }
        }
        else
        {
            // if preloaded: disable new manipulations, just load the saver
            if (index > -1 && index < manipulationsTimeSamples.Count && currentTimeSample == manipulationsTimeSamples[index])
            {
                foreach (KeyValuePair<string, float> entry in manipulationsReadings[index])
                {
                    string key = entry.Key;
                    float currentValue = entry.Value;
                    mixer.SetFloat(key, currentValue);
                }
                index++;
            }
        }
        */
        #endregion


    }


}
