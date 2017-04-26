using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaObjeto : MonoBehaviour {

    public GameObject objetoCaida;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.Equals(GameObject.Find("Personaje")))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            objetoCaida.GetComponent<Rigidbody2D>().gravityScale = 2;
            Invoke("autodestruccion", 1f);
            //GameObject.Destroy(this.gameObject);
        }
    }

    private void autodestruccion()
    {
        GameObject.Destroy(objetoCaida);
    }
}
