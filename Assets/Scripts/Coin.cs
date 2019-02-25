using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    GameController ctrl;
    const int coinValue = 5;

    // Use this for initialization
    void Start()
    {
        ctrl = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comunica al controlador que se ha capturado una moneda
        ctrl.HandlePoints(coinValue);
        //Destruye la moneda
        Destroy(gameObject);
    }
}
