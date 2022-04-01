using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class MeditationMarkersLogic : MonoBehaviour
{

    public GameObject MeditationMarker1;
    public GameObject MeditationMarker2;
    public GameObject MeditationMarker3;
    public GameObject MeditationMarker4;

    public int MeditationMarkerCounter = 1;

    public AudioSource[] audioSources = new AudioSource[6];
 
    public GameObject LocomotionSystem;

    private GameUILogic UILogicScript;

    // Start is called before the first frame update
    void Start()
    {
        UILogicScript = GetComponent<GameUILogic>();
        audioSources[0].Play();
        MeditationMarker2.SetActive(false);
        MeditationMarker3.SetActive(false);
        MeditationMarker4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (MeditationMarkerCounter)
        {
            case 1:
                if (hit.gameObject.CompareTag("MeditationMarker1"))
                {
                    audioSources[1].Play();
                    MeditationMarker1.SetActive(false);
                    LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
                    StartCoroutine(WaitAndActivateNextMarker(131));
                }
                break;
            case 2:
                if (hit.gameObject.CompareTag("MeditationMarker2"))
                {
                    audioSources[2].Play();
                    MeditationMarker2.SetActive(false);
                    LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
                    StartCoroutine(WaitAndActivateNextMarker(128));
                }
                break;
            case 3:
                if (hit.gameObject.CompareTag("MeditationMarker3"))
                {
                    audioSources[3].Play();
                    MeditationMarker3.SetActive(false);
                    LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
                    StartCoroutine(WaitAndActivateNextMarker(127));
                }
                break;
            case 4:
                if (hit.gameObject.CompareTag("MeditationMarker4"))
                {
                    audioSources[4].Play();
                    MeditationMarker4.SetActive(false);
                    LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
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

        switch (MeditationMarkerCounter)
        {
            case 1:
                LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
                MeditationMarker2.SetActive(true);
                UILogicScript.UpdateMeditationSessionText();
                MeditationMarkerCounter++;
                break;
            case 2:
                LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
                MeditationMarker3.SetActive(true);
                UILogicScript.UpdateMeditationSessionText();
                MeditationMarkerCounter++;

                break;
            case 3:
                LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
                MeditationMarker4.SetActive(true);
                UILogicScript.UpdateMeditationSessionText();
                MeditationMarkerCounter++;
                break;
            case 4:
                LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
                UILogicScript.UpdateMeditationSessionText();
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
