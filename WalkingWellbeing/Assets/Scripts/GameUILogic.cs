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
    public GameObject[] MeditationMarkers = new GameObject[4];
    private float distance;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI sessionText;
    private MeditationMarkersLogic MeditationMarkersLogicScript;

    public GameObject pauseBackground;
    public GameObject menuButton;
    public GameObject distanceBackground;
    public GameObject sessionBackground;

    private InputDevice rightHandController;

    public GameObject leftHandControllerObject;
    public GameObject rightHandControllerObject;

    private bool paused = false;
    private bool lastRightPrimaryButtonValue = false;

    AudioSource audioSourcePlaying = null;

    // Start is called before the first frame update
    void Start()
    {
        MeditationMarkersLogicScript = GetComponent<MeditationMarkersLogic>();
        paused = false;
        Time.timeScale = 1;
        pauseBackground.SetActive(false);
        menuButton.SetActive(false);
        List<InputDevice> devices = new List<InputDevice>();
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
        distance = (MeditationMarkers[MeditationMarkersLogicScript.MeditationMarkerCounter-1].transform.position - transform.position).magnitude;
        distanceText.text = "Next Marker: " + distance.ToString("F2");
    }

    public void UpdateMeditationSessionText()
    {
        sessionText.text = "Sessions Done: " + (MeditationMarkersLogicScript.MeditationMarkerCounter) + "/4";
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
            pauseBackground.SetActive(true);
            menuButton.SetActive(true);
            sessionBackground.SetActive(false);
            distanceBackground.SetActive(false);
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
            pauseBackground.SetActive(false);
            menuButton.SetActive(false);
            sessionBackground.SetActive(true);
            distanceBackground.SetActive(true);
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
