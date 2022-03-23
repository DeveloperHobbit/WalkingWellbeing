using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MediationMarkersLogic : MonoBehaviour
{

    public GameObject MeditationMarker1;
    public GameObject MeditationMarker2;
    public GameObject MeditationMarker3;
    public GameObject MeditationMarker4;

    private bool MeditationMarker1Active = false;
    private bool MeditationMarker2Active = false;
    private bool MeditationMarker3Active = false;
    private bool MeditaionMarker4Active = false;

    public AudioSource introVoice;
    public AudioSource MeditationMarker1Voice;
    public AudioSource MeditationMarker2Voice;
    public AudioSource MeditationMarker3Voice;
    public AudioSource MeditationMarker4Voice;

    public CharacterController playerController;


    // Start is called before the first frame update
    void Start()
    {
        introVoice.Play();
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
        if (MeditationMarker1Active = true)
        {
            if (hit.gameObject.CompareTag("MeditationMarker1") && hit.controller == playerController) { 
                MeditationMarker1Voice.Play();
                MeditationMarker1.SetActive(false);
            }
        }
    }
}
