using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    private void Start()
    {
        Cursor.visible = true;
    }

	public void btnReintentar()
    {
        SceneManager.LoadScene("Nivel_01");
    }

    public void btnSalir()
    {
        Application.Quit();
    }
}
