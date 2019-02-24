using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {


    Text timerGui;
    
	// Use this for initialization
	void Start () {
        timerGui = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
    public void SetTime(float time)
    {
        float hours = 0;
        float minutes = 0;
        float seconds = 0;
        int iTime = (int)time;

        hours = iTime / 3600f;

        if (hours % 1 != 0)
        {
            minutes = iTime % 3600f / 60f;

            if(minutes % 1 != 0)
            {
                seconds = iTime % 60f; 
            }
        }
        timerGui.text = new TimeSpan((int)hours, (int)minutes, (int)seconds).ToString();
    }
    
}
