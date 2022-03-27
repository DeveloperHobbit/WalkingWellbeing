using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.XR.Interaction.Toolkit;

public class MediationMarkersLogic : MonoBehaviour
{

    public GameObject MeditationMarker1;
    public GameObject MeditationMarker2;
    public GameObject MeditationMarker3;
    public GameObject MeditationMarker4;


    private bool MeditationMarker1Active = false;
    private bool MeditationMarker2Active = false;
    private bool MeditationMarker3Active = false;
    private bool MeditationMarker4Active = false;

    public AudioSource IntroVoice;
    public AudioSource MeditationMarker1Voice;
    public AudioSource MeditationMarker2Voice;
    public AudioSource MeditationMarker3Voice;
    public AudioSource MeditationMarker4Voice;


    public GameObject LocomotionSystem;


    // Start is called before the first frame update
    void Start()
    {
        IntroVoice.Play();
        MeditationMarker1Active = true;
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
        if (MeditationMarker1Active == true)
        {
            if (hit.gameObject.CompareTag("MeditationMarker1")) { 
                MeditationMarker1Voice.Play();
                MeditationMarker1.SetActive(false);
                LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
                StartCoroutine(WaitAndActivateNextMarker(131));
            }
        }
        if (MeditationMarker2Active == true)
        {
            if (hit.gameObject.CompareTag("MeditationMarker2"))
            {
                MeditationMarker2Voice.Play();
                MeditationMarker2.SetActive(false);
                LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
                StartCoroutine(WaitAndActivateNextMarker(128));
            }
        }
        if (MeditationMarker3Active == true)
        {
            if (hit.gameObject.CompareTag("MeditationMarker3"))
            {
                MeditationMarker3Voice.Play();
                MeditationMarker3.SetActive(false);
                LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
                StartCoroutine(WaitAndActivateNextMarker(127));
            }
        }
        if (MeditationMarker4Active == true)
        {
            if (hit.gameObject.CompareTag("MeditationMarker4"))
            {
                MeditationMarker4Voice.Play();
                MeditationMarker4.SetActive(false);
                LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
                StartCoroutine(WaitAndActivateNextMarker(137));
            }
        }

    }

    IEnumerator WaitAndActivateNextMarker(float TimeInSeconds)
    {
        yield return new WaitForSecondsRealtime(TimeInSeconds);

        if (MeditationMarker1Active == true)
        {
            MeditationMarker1Active = false;
            LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
            MeditationMarker2Active = true;
            MeditationMarker2.SetActive(true);
        }
        else if (MeditationMarker2Active == true)
        {
            MeditationMarker2Active = false;
            LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
            MeditationMarker3Active = true;
            MeditationMarker3.SetActive(true);
        }
        else if (MeditationMarker3Active == true)
        {
            MeditationMarker3Active = false;
            LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
            MeditationMarker4Active = true;
            MeditationMarker4.SetActive(true);
        }
        else if (MeditationMarker4Active == true)
        {
            MeditationMarker4Active = false;
            LocomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;

        }


    }

}
