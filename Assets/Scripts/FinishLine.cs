﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

    GameController ctrl;
    
    // Use this for initialization
	void Start () {
        ctrl = GameObject.FindObjectOfType<GameController>();
	}

    //Trigger para detectar cuando el jugador sale de la zona de juego
    private void OnTriggerEnter2D()
    {
        ctrl.FinishedLineReached();

    }
}
