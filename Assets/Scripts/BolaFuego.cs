using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFuego : MonoBehaviour {

    public BoxCollider2D colliderMuerte;
    public BoxCollider2D rangoAtaque;
    private CircleCollider2D jugador;
    private int danyo;
    public float velocidadMovimiento = 0.5f;

    // Use this for initialization
    void Start () {
        jugador = FindObjectOfType<PlayerController>().GetComponent<CircleCollider2D>();
        danyo = 20;
    }
	
	// Update is called once per frame
	void Update () {
        comprobarMuerte();
        comprobarMuertePersonaje();
        moverHaciaPersonaje();
    }

    // Método para comprobar si el jugador está saltando sobre la llama o no.
    private void comprobarMuerte()
    {
        if (jugador.IsTouching(colliderMuerte))
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    // Método para comprobar si el jugador colisiona con la llama.
    private void comprobarMuertePersonaje()
    {
        if (jugador.IsTouching(GetComponent<CircleCollider2D>()))
        {
            jugador.GetComponent<PlayerController>().setDanyo(danyo);
        }
    }

    // Método para moverse hacia el personaje cuando éste entre en el campo de visión.
    private void moverHaciaPersonaje()
    {
        if (rangoAtaque.IsTouching(jugador))
        {
            if (transform.position.x < jugador.gameObject.transform.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, jugador.gameObject.transform.position, velocidadMovimiento * Time.deltaTime);
                GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadMovimiento, GetComponent<Rigidbody2D>().velocity.y);
            }
            else if (transform.position.x > jugador.gameObject.transform.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, jugador.gameObject.transform.position, velocidadMovimiento * Time.deltaTime);
                GetComponent<Rigidbody2D>().velocity = new Vector2(-velocidadMovimiento, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
