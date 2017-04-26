using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInterface : MonoBehaviour {

    public Slider barraVida;
    public Text textoMunicion;
    public Text textoMoneda;
    public PlayerController personaje;

    // Use this for initialization
    void Start () {
        barraVida.minValue = 0;
        barraVida.maxValue = personaje.getVidaMax();
        barraVida.value = barraVida.maxValue;
        textoMunicion.text = personaje.getMunicion().ToString();
        textoMoneda.text = personaje.getMonedas().ToString();
    }
	
	// Update is called once per frame
	void Update () {
        actualizarBarraVida();
        actualizarTextoMunicion();
        actualizarMoneda();
    }

    // Método para asignar la vida actual del personaje a la interfaz.
    private void actualizarBarraVida()
    {
        barraVida.value = personaje.getVida();
    }

    // Método para asignar la munición actual de personaje a la interfaz.
    private void actualizarTextoMunicion()
    {
        textoMunicion.text = personaje.getMunicion().ToString();
    }

    // Método para asignar las monedas actuales del personaje a la interfaz.
    private void actualizarMoneda()
    {
        textoMoneda.text = personaje.getMonedas().ToString();
    }
}
