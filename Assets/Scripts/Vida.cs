using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour {

    private int vida;

    // Use this for initialization
    void Start()
    {
        vida = 2000;
    }

    // Método para controlar si el GameObject que colisiona es o no el personaje.
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.Equals(GameObject.Find("Personaje")))
        {
            coll.gameObject.GetComponent<PlayerController>().setVida(vida);
            GameObject.Destroy(this.gameObject);
        }
    }
}
