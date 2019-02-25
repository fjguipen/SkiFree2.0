using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private GameObject player;
    private Vector3 offset;

    // Use this for initialization
    void Start () 
    {
        //Obtiene la psocion actual del jugador
        player = GameObject.FindWithTag("Player");
        //Guarda la distancia entre el jugador y la cámara
        offset = transform.position - player.transform.position;
    }
    
    
    void LateUpdate () 
    {
        //Mantiene la cámara pegada al jugador
        transform.position = player.transform.position + offset;
    }
}