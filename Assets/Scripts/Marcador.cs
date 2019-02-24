using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marcador : MonoBehaviour {

    public int puntuacion;
    Text display;
    ParticleSystem effect;
    

	// Use this for initialization
	void Start () {

        puntuacion = 0;
        display = gameObject.GetComponent<Text>();
        effect = gameObject.GetComponentInChildren<ParticleSystem>();

    }

    public void AddPoints(int puntos)
    {
        puntuacion += puntos;
        UpdateMarcador();
    }

    public void DoublePoints()
    {
        puntuacion = puntuacion * 2;
        UpdateMarcador();
        effect.Play();
    }

    private void UpdateMarcador()
    {
        display.text = puntuacion.ToString();
    }
}
