using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaMala : MonoBehaviour {

    private int danyo;
    public LayerMask mascaraEscalera;
    public LayerMask mascaraRangoAtaqueEnemigo;
    public LayerMask mascaraActivadorCaida;
    public LayerMask mascaraEnemigo;
    public LayerMask mascaraSuelo;

    // Use this for initialization
    void Start()
    {
        danyo = 500;
        Invoke("autodestruccion", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Método para que la bala se destruya automáticamente.
    private void autodestruccion()
    {
        GameObject.Destroy(this.gameObject);
    }

    // Método para que la bala se destruya al colisionar con cualquier objeto.
    private void OnTriggerEnter2D(Collider2D coll)
    {
        PlayerController personaje = coll.gameObject.GetComponent<PlayerController>();
        if (!GetComponent<CircleCollider2D>().IsTouchingLayers(mascaraEnemigo) && !GetComponent<CircleCollider2D>().IsTouchingLayers(mascaraActivadorCaida)
                && !GetComponent<CircleCollider2D>().IsTouchingLayers(mascaraEscalera) && !GetComponent<CircleCollider2D>().IsTouchingLayers(mascaraRangoAtaqueEnemigo))
        {
            GameObject.Destroy(this.gameObject);
        }
        else if (personaje != null)
        {
            personaje.setDanyo(danyo);
            GameObject.Destroy(this.gameObject);
        } else if (GetComponent<CircleCollider2D>().IsTouchingLayers(mascaraSuelo))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
