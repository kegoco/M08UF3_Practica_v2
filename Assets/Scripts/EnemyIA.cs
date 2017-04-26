using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIA : MonoBehaviour {

    private int vidaMax;
    private int vida;

    public GameObject recompensaMunicion;
    public GameObject recompensaVida;

    public BoxCollider2D rangoAtaque;
    public GameObject personaje;
    private Animator animator;
    private int direccion;
    public float velocidadMovimiento = 0.5f;

    public GameObject balaMala;
    private bool atacando;
    public AudioSource sonidoDisparo;

    public Slider barraVida;

    // Use this for initialization
    void Start () {
        vidaMax = 1000;
        vida = vidaMax;
        animator = GetComponent<Animator>();
        direccion = 0;
        atacando = false;
        barraVida.maxValue = vidaMax;
        barraVida.minValue = 0;
        barraVida.value = vida;
    }

    private void FixedUpdate()
    {
        animator.SetInteger("Direction", direccion);
        barraVida.value = vida;
    }

    // Update is called once per frame
    void Update () {
        moverHaciaPersonaje();
        cambiarAnimacion();
        atacarPersonaje();
    }

    // Método para recibir daño y restarselo a la vida.
    public void setDanyo(int danyo)
    {
        vida -= danyo;
        comprobarMuerte();
    }

    // Método para comprobar si el enemigo está muerto; en ese caso antes de ser destruido podrá dejar una recompensa.
    private void comprobarMuerte()
    {
        if (vida <= 0)
        {
            int recompensa = Random.Range(1, 100);
            if (recompensa > 0 && recompensa < 25)
            {
                GameObject bala = Instantiate(recompensaMunicion, new Vector2(this.gameObject.transform.position.x,
                    this.gameObject.transform.position.y + 1f), Quaternion.identity);
            }
            else if (recompensa > 24 && recompensa < 50)
            {
                GameObject bala = Instantiate(recompensaVida, new Vector2(this.gameObject.transform.position.x,
                    this.gameObject.transform.position.y + 1f), Quaternion.identity);
            }
            GameObject.Destroy(this.gameObject);
        }
    }

    // Método para moverse hacia el personaje cuando éste entre en el campo de visión.
    private void moverHaciaPersonaje()
    {
        if (rangoAtaque.IsTouching(personaje.GetComponent<CircleCollider2D>()))
        {
            if (transform.position.x < personaje.transform.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, personaje.transform.position, velocidadMovimiento * Time.deltaTime);
                GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadMovimiento, GetComponent<Rigidbody2D>().velocity.y);
            } else if (transform.position.x > personaje.transform.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, personaje.transform.position, velocidadMovimiento * Time.deltaTime);
                GetComponent<Rigidbody2D>().velocity = new Vector2(-velocidadMovimiento, GetComponent<Rigidbody2D>().velocity.y);
            }
        } else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    // Método para cambiar la animación en referencia a la posición del personaje cuando éste es avistado.
    private void cambiarAnimacion()
    {
        if (rangoAtaque.IsTouching(personaje.GetComponent<CircleCollider2D>()))
        {
            // Comprobar la posición X.
            if (transform.position.x < personaje.transform.position.x - 0.3f)
            {
                direccion = 3;
            }
            else if (transform.position.x > personaje.transform.position.x + 0.3f)
            {
                direccion = 1;
            } else
            {
                // Comprobar la posición Y en caso de que las X de los dos sean iguales.
                if (transform.position.y < personaje.transform.position.y)
                {
                    direccion = 2;
                } else
                {
                    direccion = 0;
                }
            }
        }
    }

    // Método para disparar al personaje cuando éste entre en su campo de visión.
    private void atacarPersonaje()
    {
        if (rangoAtaque.IsTouching(personaje.GetComponent<CircleCollider2D>()) && !atacando)
        {
            atacando = true;
            sonidoDisparo.Play();
            GameObject balaMalita = Instantiate(balaMala, new Vector2(this.gameObject.transform.position.x,
                    this.gameObject.transform.position.y), Quaternion.identity);

            switch (direccion)
            {
                case 0:
                    balaMalita.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -500));
                    break;
                case 1:
                    balaMalita.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 0));
                    break;
                case 2:
                    balaMalita.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
                    break;
                case 3:
                    balaMalita.GetComponent<Rigidbody2D>().AddForce(new Vector2(500, 0));
                    break;
            }

            StartCoroutine(tiempoEspera());  // Llama a una corrutina para hacer un tiempo de espera entre bala y bala.
        }
    }

    // Corrutina para esperar los segundos que le indiquemos.
    IEnumerator tiempoEspera()
    {
        yield return new WaitForSeconds(0.7f);
        atacando = false;
    }
}
