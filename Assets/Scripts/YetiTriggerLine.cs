using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiTriggerLine : MonoBehaviour
{

    GameController ctrl;

    // Use this for initialization
    void Start()
    {
        ctrl = FindObjectOfType<GameController>();
    }

    //Trigger para detectar cuando el jugador pasa por la linea de activacion del yeti
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comunica al controlador que debe soltar al yeti
        ctrl.ReleaseTheYeti();

    }
}
