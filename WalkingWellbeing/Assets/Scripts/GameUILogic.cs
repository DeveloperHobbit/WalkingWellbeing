using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GameUILogic : MonoBehaviour
{
    private GameObject[] meditationMarkers = new GameObject[4];
    private float markerDistance;
    private TextMeshProUGUI markerDistanceText;
    private TextMeshProUGUI sessionNumberText;
    private MeditationMarkersLogic MeditationMarkersLogicScript;

    private GameObject pausedTextBackground;
    private GameObject menuButton;
    private GameObject markerDistanceTextBackground;
    private GameObject sessionNumberTextBackground;

    private InputDevice rightHandController;

    private GameObject leftHandControllerObject;
    private GameObject rightHandControllerObject;

    private bool paused = false;
    private bool lastRightPrimaryButtonValue = false;

    AudioSource audioSourcePlaying;

    List<InputDevice> devices = new List<InputDevice>();

    // Start is called before the first frame update
    void Start()
    {
        meditationMarkers[0] = GameObject.Find("MeditationMarker1");
        meditationMarkers[1] = GameObject.Find("MeditationMarker2");
        meditationMarkers[2] = GameObject.Find("MeditationMarker3");
        meditationMarkers[3] = GameObject.Find("MeditationMarker4");

        markerDistanceText = GameObject.Find("nextMarkerText").GetComponent<TextMeshProUGUI>();
        sessionNumberText = GameObject.Find("sessionNumberText").GetComponent<TextMeshProUGUI>();
       
        MeditationMarkersLogicScript = GetComponent<MeditationMarkersLogic>();

        pausedTextBackground = GameObject.Find("pauseBackground");
        menuButton = GameObject.Find("menuButton");
        markerDistanceTextBackground = GameObject.Find("nextMarkerBackground");
        sessionNumberTextBackground = GameObject.Find("sessionNumberBackground");

        leftHandControllerObject = GameObject.Find("LeftHand Controller");
        rightHandControllerObject = GameObject.Find("RightHand Controller");

        paused = false;
        Time.timeScale = 1;
        pausedTextBackground.SetActive(false);
        menuButton.SetActive(false);
        InputDevices.GetDevices(devices);
        rightHandController = devices[2];
        leftHandControllerObject.GetComponent<XRInteractorLineVisual>().enabled = false;
        rightHandControllerObject.GetComponent<XRInteractorLineVisual>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMarkerDistanceText();
        CheckForPauseInput();
    }

    void UpdateMarkerDistanceText()
    {
        markerDistance = (meditationMarkers[MeditationMarkersLogicScript.meditationMarkerCounter-1].transform.position - transform.position).magnitude;
        markerDistanceText.text = "Next Marker: " + markerDistance.ToString("F2");
    }

    public void UpdateMeditationSessionText()
    {
        sessionNumberText.text = "Sessions Done: " + (MeditationMarkersLogicScript.meditationMarkerCounter) + "/4";
    }

    void CheckForPauseInput()
    {
        rightHandController.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPrimaryButtonValue);

        if (rightPrimaryButtonValue == true && lastRightPrimaryButtonValue != rightPrimaryButtonValue)
        {
            TogglePauseGame();
        }
        lastRightPrimaryButtonValue = rightPrimaryButtonValue;
    }

    void TogglePauseGame()
    {
        if(paused == false)
        {
            paused = true;
            pausedTextBackground.SetActive(true);
            menuButton.SetActive(true);
            sessionNumberTextBackground.SetActive(false);
            markerDistanceTextBackground.SetActive(false);
            Time.timeScale = 0;

            leftHandControllerObject.GetComponent<XRInteractorLineVisual>().enabled = true;
            rightHandControllerObject.GetComponent<XRInteractorLineVisual>().enabled = true;

            foreach (AudioSource audioSource in MeditationMarkersLogicScript.audioSources)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Pause();
                    audioSourcePlaying = audioSource;
                }
            }
        }
        else
        {
            paused = false;
            pausedTextBackground.SetActive(false);
            menuButton.SetActive(false);
            sessionNumberTextBackground.SetActive(true);
            markerDistanceTextBackground.SetActive(true);
            Time.timeScale = 1;

            leftHandControllerObject.GetComponent<XRInteractorLineVisual>().enabled = false;
            rightHandControllerObject.GetComponent<XRInteractorLineVisual>().enabled = false;

            if (audioSourcePlaying != null) {
                audioSourcePlaying.UnPause();
                audioSourcePlaying = null;
            }
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
