using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class MeditationMarkersLogic : MonoBehaviour
{
    private GameObject[] meditationMarkers = new GameObject[4];
    public int meditationMarkerCounter = 1;
    public AudioSource[] audioSources = new AudioSource[6];
    private GameObject locomotionSystem;
    private GameUILogic uiLogicScript;

    // Start is called before the first frame update
    void Start()
    {
        meditationMarkers[0] = GameObject.Find("MeditationMarker1");
        meditationMarkers[1] = GameObject.Find("MeditationMarker2");
        meditationMarkers[2] = GameObject.Find("MeditationMarker3");
        meditationMarkers[3] = GameObject.Find("MeditationMarker4");

        audioSources[0] = GameObject.Find("Intro").GetComponent<AudioSource>();
        audioSources[1] = GameObject.Find("Meditation1").GetComponent<AudioSource>();
        audioSources[2] = GameObject.Find("Meditation2").GetComponent<AudioSource>();
        audioSources[3] = GameObject.Find("Meditation3").GetComponent<AudioSource>();
        audioSources[4] = GameObject.Find("Meditation4").GetComponent<AudioSource>();
        audioSources[5] = GameObject.Find("Outro").GetComponent<AudioSource>();

        locomotionSystem = GameObject.Find("Locomotion System");

        uiLogicScript = GetComponent<GameUILogic>();
        audioSources[0].Play();
        meditationMarkers[1].SetActive(false);
        meditationMarkers[2].SetActive(false);
        meditationMarkers[3].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (meditationMarkerCounter)
        {
            case 1:
                if (hit.gameObject.CompareTag("MeditationMarker1"))
                {
                    audioSources[1].Play();
                    meditationMarkers[0].SetActive(false);
                    locomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
                    StartCoroutine(WaitAndActivateNextMarker(131));
                }
                break;
            case 2:
                if (hit.gameObject.CompareTag("MeditationMarker2"))
                {
                    audioSources[2].Play();
                    meditationMarkers[1].SetActive(false);
                    locomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
                    StartCoroutine(WaitAndActivateNextMarker(128));
                }
                break;
            case 3:
                if (hit.gameObject.CompareTag("MeditationMarker3"))
                {
                    audioSources[3].Play();
                    meditationMarkers[2].SetActive(false);
                    locomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
                    StartCoroutine(WaitAndActivateNextMarker(127));
                }
                break;
            case 4:
                if (hit.gameObject.CompareTag("MeditationMarker4"))
                {
                    audioSources[4].Play();
                    meditationMarkers[3].SetActive(false);
                    locomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
                    StartCoroutine(WaitAndActivateNextMarker(210));
                }
                break;
            default:
                break;
        }
    }

    IEnumerator WaitAndActivateNextMarker(float TimeInSeconds)
    {
        yield return new WaitForSeconds(TimeInSeconds);

        switch (meditationMarkerCounter)
        {
            case 1:
                locomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
                meditationMarkers[1].SetActive(true);
                uiLogicScript.UpdateMeditationSessionText();
                meditationMarkerCounter++;
                break;
            case 2:
                locomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
                meditationMarkers[2].SetActive(true);
                uiLogicScript.UpdateMeditationSessionText();
                meditationMarkerCounter++;

                break;
            case 3:
                locomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
                meditationMarkers[3].SetActive(true);
                uiLogicScript.UpdateMeditationSessionText();
                meditationMarkerCounter++;
                break;
            case 4:
                locomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
                uiLogicScript.UpdateMeditationSessionText();
                StartCoroutine(PlayOutroAndEndGame());
                break;
            default:
                break;
        }
    }

    IEnumerator PlayOutroAndEndGame()
    {
        audioSources[5].Play();
        yield return new WaitForSeconds(26);
        SceneManager.LoadScene("MenuScene");
    }
}
