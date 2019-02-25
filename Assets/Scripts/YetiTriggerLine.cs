using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiTriggerLine : MonoBehaviour {

    GameController ctrl;

	// Use this for initialization
	void Start () {
        ctrl = FindObjectOfType<GameController>();
	}

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ctrl.ReleaseTheYeti();
        
    }
}
