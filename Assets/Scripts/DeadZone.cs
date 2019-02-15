using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{


    GameObject camara;

    // Use this for initialization
    void Start()
    {
        camara = GameObject.Find("Main Camera");
    }

    //Trigger para detectar cuando el jugador sale de la zona de juego
    private void OnTriggerEnter2D(Collider2D other)
    {
        camara.GetComponent<CameraMovement>().enabled = false;
        
    }
}
