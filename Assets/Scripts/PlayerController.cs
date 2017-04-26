using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    private Animator animator;

    public LayerMask mascaraPinchosCaida;
    public LayerMask mascaraSuelo;
    public LayerMask mascaraMoneda;
    public Transform comprobadorSuelo;
    private float comprobadorRadio = 0.07f;
    private bool tocandoSuelo;
    
    public float velocidadMovimiento = 3f;
    public float fuerzaSalto = 100f;
    private int direccion;

    public GameObject balaBuena;
    private bool atacando;

    public LayerMask mascaraEscalera;
    private bool tocandoEscalera;

    public AudioSource sonidoDisparo;
    public AudioSource sonidoMunicion;
    public AudioSource sonidoVida;
    public AudioSource sonidoSinMunicion;
    public AudioSource sonidoMoneda;

    private int vidaMax;
    private int vida;
    private int municion;
    private int monedas;
    private int totalMonedas;


    private void Awake()
    {
        vidaMax = 5000;
        vida = vidaMax;
        municion = 100;
        monedas = 0;
    }

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        tocandoSuelo = true;
        direccion = 0;
        tocandoEscalera = false;
        atacando = false;
        totalMonedas = GameObject.FindGameObjectsWithTag("Moneda").Length;  // Cuenta las monedas que haya en la escena.
    }
    
    private void FixedUpdate()
    {
        animarPersonaje();
        tocandoSuelo = Physics2D.OverlapCircle(comprobadorSuelo.position, comprobadorRadio, mascaraSuelo);
        tocandoEscalera = Physics2D.OverlapCircle(comprobadorSuelo.position, comprobadorRadio, mascaraEscalera);
    }

    // Update is called once per frame
    void Update () {
        moverPersonaje();
        saltarPersonaje();
        atacarPersonaje();
        victoriaPersonaje();
    }

    // Método para mover al personaje.
    private void moverPersonaje()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadMovimiento, GetComponent<Rigidbody2D>().velocity.y);
        } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-velocidadMovimiento, GetComponent<Rigidbody2D>().velocity.y);
        } else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && tocandoEscalera)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, velocidadMovimiento);
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && tocandoEscalera)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -velocidadMovimiento);
        } else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    // Método para hacer saltar al personaje cuando éste esté tocando el suelo.
    private void saltarPersonaje()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X)) && tocandoSuelo)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, fuerzaSalto));
        }
    }

    // Método para cambiar la animación del personaje al moverse.
    private void animarPersonaje()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        if (vertical > 0)
        {
            direccion = 2;
            animator.SetInteger("Direction", direccion);
        }
        else if (vertical < 0)
        {
            direccion = 0;
            animator.SetInteger("Direction", direccion);
        }
        else if (horizontal > 0)
        {
            direccion = 3;
            animator.SetInteger("Direction", direccion);
        }
        else if (horizontal < 0)
        {
            direccion = 1;
            animator.SetInteger("Direction", direccion);
        }
    }

    // Método para hacer que el personaje dispare objetos (en este caso disparará balas).
    private void atacarPersonaje()
    {
        
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Z)) && !atacando && municion > 0 && !MenuPausa.getJuegoPausado())
        {
            atacando = true;
            sonidoDisparo.Play();
            municion -= 1;

            GameObject bala = null;
            switch (direccion)
            {
                case 0:
                    bala = Instantiate(balaBuena, new Vector2(transform.position.x - 0.15f,
                            transform.position.y - 0.025f), Quaternion.identity);
                    break;
                case 1:
                    bala = Instantiate(balaBuena, new Vector2(transform.position.x - 0.15f,
                            transform.position.y - 0.029f), Quaternion.identity);
                    break;
                case 2:
                    bala = Instantiate(balaBuena, new Vector2(transform.position.x + 0.15f,
                            transform.position.y - 0.025f), Quaternion.identity);
                    break;
                case 3:
                    bala = Instantiate(balaBuena, new Vector2(transform.position.x + 0.15f,
                            transform.position.y - 0.029f), Quaternion.identity);
                    break;
            }

            switch (direccion)
            {
                case 0:
                    bala.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -500));
                    break;
                case 1:
                    bala.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 0));
                    break;
                case 2:
                    bala.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
                    break;
                case 3:
                    bala.GetComponent<Rigidbody2D>().AddForce(new Vector2(500, 0));
                    break;
            }

            StartCoroutine(tiempoEspera());  // Llama a una corrutina para hacer un tiempo de espera entre bala y bala.
        } else if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Z)) && municion <= 0 && !MenuPausa.getJuegoPausado())
        {
            if (!sonidoSinMunicion.isPlaying)
            {
                sonidoSinMunicion.Play();
            }
        }
    }

    // Corrutina para esperar los segundos que le indiquemos.
    IEnumerator tiempoEspera()
    {
        yield return new WaitForSeconds(0.1f);
        atacando = false;
    }

    // Método para controlar las colisiones entrantes.
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (GetComponent<CircleCollider2D>().IsTouchingLayers(mascaraPinchosCaida))
        {
            vida -= 1000;
        }

        if (vida <= 0)
        {
            SceneManager.LoadScene("Fin_juego");
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (GetComponent<CircleCollider2D>().IsTouchingLayers(mascaraMoneda))
        {
            monedas++;
            sonidoMoneda.Play();
            GameObject.Destroy(coll.gameObject);
        }
    }

    // Métodos para poder visualizar los valores de los atributos privados de ésta clase.
    public int getVidaMax()
    {
        return vidaMax;
    }
    public int getVida()
    {
        return vida;
    }
    public int getMunicion()
    {
        return municion;
    }
    public int getMonedas()
    {
        return monedas;
    }

    // Métodos para poder cambiar los valores de los atributos privados de ésta clase.
    public void setMunicion(int municion)
    {
        sonidoMunicion.Play();
        this.municion += municion;
    }
    public void setVida(int vida)
    {
        sonidoVida.Play();
        if (vida > (vidaMax - this.vida))
        {
            this.vida = vidaMax;
        } else
        {
            this.vida += vida;
        }
    }
    public void setDanyo(int danyo)
    {
        vida -= danyo;
        if (vida <= 0)
        {
            SceneManager.LoadScene("Fin_juego");
        }
    }

    // Método para abrir la escena de victoria en caso de que el jugador recoja todas las modenas.
    public void victoriaPersonaje()
    {
        if (monedas == totalMonedas)
        {
            SceneManager.LoadScene("Victoria");
        }
    }
}
