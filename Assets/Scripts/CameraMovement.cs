﻿using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public GameObject player;


    private Vector3 offset;

    // Use this for initialization
    void Start () 
    {
        player = GameObject.FindWithTag("Player");
        offset = transform.position - player.transform.position;
    }
    
    
    void LateUpdate () 
    {
        
        transform.position = player.transform.position + offset;
    }
}