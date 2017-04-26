using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaAlVacio : MonoBehaviour {

    public float coordX;
    public float coordY;
    private int danyoCaida;
    private GameObject personaje;

	// Use this for initialization
	void Start () {
        personaje = GameObject.Find("Personaje");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // 
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.Equals(personaje))
        {
            personaje.GetComponent<PlayerController>().setDanyo(danyoCaida);
            personaje.transform.position = new Vector2(coordX, coordY);
        }
    }
}
