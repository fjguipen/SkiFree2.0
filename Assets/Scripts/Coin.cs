using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    GameController ctrl;
    const int coinValue = 5;
    
	// Use this for initialization
	void Start () {
        ctrl = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	    	
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ctrl.HandlePoints(coinValue);
        Destroy(gameObject);
    }
}
