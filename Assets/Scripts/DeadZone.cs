using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{


    GameObject camara;
    GameController ctrl;

    // Use this for initialization
    void Start()
    {
        //camara = GameObject.Find("Main Camera");
        ctrl = GameController.FindObjectOfType<GameController>();
    }

    //Trigger para detectar cuando el jugador sale de la zona de juego
    private void OnTriggerEnter2D(Collider2D other)
    {

        ctrl.PlayerAbyssFall();
        
    }
}
