using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalDisplay : MonoBehaviour {

    public Text display;

	// Use this for initialization
	void Start () {
        display = gameObject.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
