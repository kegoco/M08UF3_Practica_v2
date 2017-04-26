using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion : MonoBehaviour {

    private int municion;

	// Use this for initialization
	void Start () {
        municion = 50;
    }

    // Método para controlar si el GameObject que colisiona es o no el personaje.
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.Equals(GameObject.Find("Personaje"))) {
            coll.gameObject.GetComponent<PlayerController>().setMunicion(municion);
            GameObject.Destroy(this.gameObject);
        }
    }
}
