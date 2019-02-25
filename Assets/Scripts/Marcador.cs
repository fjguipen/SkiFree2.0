using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marcador : MonoBehaviour
{

    public int puntuacion;
    Text display;
    ParticleSystem effect;


    // Use this for initialization
    void Start()
    {
        //Inicia la puntuacion a 0
        puntuacion = 0;
        display = gameObject.GetComponent<Text>();
        effect = gameObject.GetComponentInChildren<ParticleSystem>();

    }

    //Al ser llamado agrega el valor pasado por parámetro al computo total de puntos
    public void AddPoints(int puntos)
    {
        puntuacion += puntos;
        //Y actualiza el display
        UpdateMarcador();
    }

    //Dobla los puntos y actualiza el marcador
    public void DoublePoints()
    {
        puntuacion = puntuacion * 2;
        UpdateMarcador();
        
        //Emite el efecto sobre el marcador cuando se doblan los puntos
        effect.Play();
    }

    //Refleja la puntuación actual en la pantalla
    private void UpdateMarcador()
    {
        display.text = puntuacion.ToString();
    }
}
