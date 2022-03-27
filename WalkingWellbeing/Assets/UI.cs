using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Update()
    {
        RectTransform mrect = GetComponent<RectTransform>();
        Vector2 apos = mrect.anchoredPosition;
        float xpos = apos.x;
        xpos = Mathf.Clamp(xpos, 0, Screen.width - mrect.sizeDelta.x);
        apos.x = xpos;
        mrect.anchoredPosition = apos;
    }
}
