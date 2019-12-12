using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    float movVert;
    float movHoriz;
    public GameObject MensFInal;
    public GameObject boton;
    bool juego = true;
    void Start()
    {
        MensFInal.SetActive(false);
        boton.SetActive(false);
    }


    void Update()
    {
        if (juego)
        {
            movVert = Input.GetAxis("Vertical");
            movHoriz = Input.GetAxis("Horizontal");
            transform.Translate(movHoriz * 4 * Time.deltaTime, 0, movVert * 4 * Time.deltaTime); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Llegada")
        {
            MensFInal.SetActive(true);
            boton.SetActive(true);
            CompEnemig.juego = false;
        }
    }

    public void RecargaScena()
    {
        SceneManager.LoadScene(0);
    }
}
