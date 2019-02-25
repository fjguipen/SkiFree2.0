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
        ctrl = FindObjectOfType<GameController>();
    }

    //Trigger para detectar cuando el jugador sale de la zona de juego
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Comunica al controlador que el jugador se ha caido fuera del mapa
        ctrl.PlayerAbyssFall();
        
    }
}
