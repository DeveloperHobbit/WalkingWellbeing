using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MeditationMarkersLogic : MonoBehaviour
{

    public GameObject MeditationMarker1;
    public GameObject MeditationMarker2;
    public GameObject MeditationMarker3;
    public GameObject MeditationMarker4;

    public int MeditationMarkerCounter = 1;

    public AudioSource IntroVoice;
    public AudioSource MeditationMarker1Voice;
    public AudioSource MeditationMarker2Voice;
    public AudioSource MeditationMarker3Voice;
    public AudioSource MeditationMarker4Voice;
    public AudioSource OutroVoice;

    public GameObject LocomotionSystem;

    private UILogic UILogicScript;

    // Start is called before the first frame update
    void Start()
    {
        UILogicScript = GetComponent<UILogic>();
        IntroVoice.Play();
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
                    MeditationMarker1Voice.Play();
                    MeditationMarker1.SetActive(false);
                    LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
                    StartCoroutine(WaitAndActivateNextMarker(131));
                }
                break;
            case 2:
                if (hit.gameObject.CompareTag("MeditationMarker2"))
                {
                    MeditationMarker2Voice.Play();
                    MeditationMarker2.SetActive(false);
                    LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
                    StartCoroutine(WaitAndActivateNextMarker(128));
                }
                break;
            case 3:
                if (hit.gameObject.CompareTag("MeditationMarker3"))
                {
                    MeditationMarker3Voice.Play();
                    MeditationMarker3.SetActive(false);
                    LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
                    StartCoroutine(WaitAndActivateNextMarker(127));
                }
                break;
            case 4:
                if (hit.gameObject.CompareTag("MeditationMarker4"))
                {
                    MeditationMarker4Voice.Play();
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
        yield return new WaitForSecondsRealtime(TimeInSeconds);

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
        OutroVoice.Play();
        yield return new WaitForSecondsRealtime(26);
        Application.Quit();
    }
}
