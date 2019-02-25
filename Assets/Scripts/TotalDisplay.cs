using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Clase por convenciecia, utilizamos su atributo display desde el controlador

public class TotalDisplay : MonoBehaviour {

    public Text display;

	// Use this for initialization
	void Start () {
        display = gameObject.GetComponentInChildren<Text>();
	}
	
}
