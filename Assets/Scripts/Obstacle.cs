using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    GameController controller;

	// Use this for initialization
	void Start () {
        controller = GameObject.FindObjectOfType<GameController>();
	}

    //Trigger para detectar cuando el jugador choca contra algun obstáculo
    private void OnCollisionEnter2D ()
    {
        //Comunica al controllador que el jugador ha de ser destruido
        controller.DestroyedByObstacle();
    }
}
