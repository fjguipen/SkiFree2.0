using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

    GameController ctrl;
    
    // Use this for initialization
	void Start () {
        ctrl = FindObjectOfType<GameController>();
	}

    //Trigger para detectar cuando el jugador llega a la meta
    private void OnTriggerEnter2D()
    {
        //Comunica al controlador que se ha alcanzado la linea de meta
        ctrl.FinishedLineReached();

    }
}
