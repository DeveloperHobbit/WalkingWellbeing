using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;

public class UILogic : MonoBehaviour
{
    public GameObject[] MeditationMarkers = new GameObject[4];
    private float distance;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI sessionText;
    private MeditationMarkersLogic MeditationMarkersLogicScript;

    public GameObject pauseBackground;
    public GameObject homeButton;
    public GameObject distanceBackground;
    public GameObject sessionBackground;

    private InputDevice rightHandController;
    private bool paused = false;
    private bool lastRightPrimaryButtonValue = false;

    // Start is called before the first frame update
    void Start()
    {
        MeditationMarkersLogicScript = GetComponent<MeditationMarkersLogic>();
      
        pauseBackground.SetActive(false);
        homeButton.SetActive(false);
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        rightHandController = devices[2];
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
            homeButton.SetActive(true);
            sessionBackground.SetActive(false);
            distanceBackground.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseBackground.SetActive(false);
            homeButton.SetActive(false);
            sessionBackground.SetActive(true);
            distanceBackground.SetActive(true);
            Time.timeScale = 1;

        }



    }

}
