using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour {

    private static bool juegoPausado;
    public GameObject menuPausaCanvas;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (juegoPausado)
        {
            menuPausaCanvas.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
        }
        else
        {
            menuPausaCanvas.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            juegoPausado = !juegoPausado;
        }
    }

    // Método para salir del menú pausa y poder continuar la partida.
    public void btnContinuar()
    {
        juegoPausado = false;
    }

    // Método para poder reiniciar el nivel actual.
    public void btnReiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Método para poder salir del videojuego.
    public void btnSalir()
    {
        Application.Quit();
    }

    // Métodos para poder visualizar los valores de los atributos privados de ésta clase.
    public static bool getJuegoPausado()
    {
        return juegoPausado;
    }
}
