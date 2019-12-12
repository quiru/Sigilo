using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CompEnemig : MonoBehaviour
{
    [SerializeField] private float rangoVision;
    [SerializeField] private Transform posicionJugador;
    private Transform posicionEnemigo;
    public static bool juego = true;
    public GameObject texLose;
    public GameObject boton;
    float mov = 1.5f;

    void Awake()
    {
        posicionEnemigo = transform;
        texLose.SetActive(false);
        boton.SetActive(false);
    }

    void Start()
    {
        StartCoroutine("cambiaMov");
    }


    void Update()
    {
        if (juego == true)
        {
            if (estaViendoJugador(posicionJugador.position))
            {
                texLose.SetActive(true);
                boton.SetActive(true);
                juego = false;
            }
        }

        transform.Translate(0, 0, mov * Time.deltaTime);
    }

    private bool estaViendoJugador(Vector3 posJugador)
    {
        Vector3 desplazamiento = posJugador - posicionEnemigo.position;
        float distanciaAJugador = desplazamiento.magnitude;

        if (distanciaAJugador <= rangoVision)
        {
            float prodPunto = Vector3.Dot(posicionEnemigo.forward, desplazamiento);
            if (prodPunto >= 0.5f)
            {
                int layerMask = 1 << 2;
                layerMask = ~layerMask;

                RaycastHit obj;
                if (Physics.Raycast(posicionEnemigo.position, desplazamiento.normalized, out obj, rangoVision, layerMask))
                {
                    Debug.DrawRay(posicionEnemigo.position, desplazamiento.normalized * obj.distance, Color.red);
                    
                    if (obj.collider.GetComponent<Jugador>())
                    {
                        Debug.DrawRay(posicionEnemigo.position, desplazamiento.normalized * obj.distance, Color.green);
                        print("lo vio");
                        return true;
                    }
                }
            }
        }

        return false;
    }

    IEnumerator cambiaMov()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            mov = 0;
            transform.eulerAngles += new Vector3(0, 180, 0);
            yield return new WaitForSeconds(3);
            mov = 1.5f; 
        }
    }
}
