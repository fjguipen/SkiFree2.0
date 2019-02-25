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

    //Método para pasar el tiempo transcurrido(segundos) a formato reloj(horas:minutos:segundos)
    public void SetTime(float time)
    {
        float hours = 0;
        float minutes = 0;
        float seconds = 0;
        int iTime = (int)time;

        //Vemos cuantas horas han pasado
        hours = iTime / 3600f;
        //Anoser que los segundos en total sumen horas exactas (sin minutos ni segundos de más)
        if (hours % 1 != 0)
        {
            //Vemos cuantos minutos han pasado (descontando las horas transcurridas)
            minutes = iTime % 3600f / 60f;
            //Anoser que los segundos restantes sumen minutos exactos(sin segundos de más)
            if (minutes % 1 != 0)
            {
                //Vemos cuantos segundos quedan tras extraer los minutos
                seconds = iTime % 60f; 
            }
        } 

        //Actualizamos el display con el tiempo actual
        timerGui.text = new TimeSpan((int)hours, (int)minutes, (int)seconds).ToString();
    }
    
}
