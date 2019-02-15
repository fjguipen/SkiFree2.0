using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

	
    
    // Use this for initialization
	void Start () {
		
	}

    //Trigger para detectar cuando el jugador sale de la zona de juego
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Player>().StopMovement();

    }
}
