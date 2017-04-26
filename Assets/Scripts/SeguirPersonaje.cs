using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirPersonaje : MonoBehaviour {

    public Transform personaje;
    public float separacion = 0f;
    private bool camaraPosArriba = false;

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3(personaje.position.x + separacion, transform.position.y, transform.position.z);

        if (personaje.position.y > 2f && !camaraPosArriba)
        {
            camaraPosArriba = true;
            StartCoroutine(camaraArriba());
        } else if (personaje.position.y < 2f && camaraPosArriba)
        {
            camaraPosArriba = false;
            StartCoroutine(camaraAbajo());
        }
    }

    // Corrutina para subir la cámara.
    IEnumerator camaraArriba()
    {
        for (float i = transform.position.y; i < 4; i = i + 0.2f)
        {
            transform.position = new Vector3(personaje.position.x + separacion, i,
                transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Corrutina para bajar la cámara.
    IEnumerator camaraAbajo()
    {
        for (float i = transform.position.y; i > 0; i = i - 0.2f)
        {
            transform.position = new Vector3(personaje.position.x + separacion, i,
                transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
