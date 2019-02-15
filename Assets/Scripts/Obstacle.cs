using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    //Trigger para detectar cuando el jugador sale de la zona de juego
    private void OnCollisionEnter2D (Collision2D other)
    {
        Debug.Log(other.contacts[0].normal);  
        other.gameObject.GetComponent<Player>().Kill();
    }
}
