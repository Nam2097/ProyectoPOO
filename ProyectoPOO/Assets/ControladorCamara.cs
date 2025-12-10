using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections.Generic;

public class ControladorCamara : MonoBehaviour
{
    public int dondeEsta=1;
    public GameObject pausa;
    private bool juegoPausado=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pausa.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!juegoPausado)
            {
                pausa.SetActive(true);
                Time.timeScale = 0f; 
                juegoPausado = true;
            }
            
        }
    }

    public void cambiarSitio(int numSitio) //comienza en 1
    {
        transform.position = new Vector3((float)(numSitio-1)*10f,0f,-10f);
        dondeEsta=numSitio;
    }
    public void continuar()
    {
        pausa.SetActive(false);
        Time.timeScale = 1f; 
        juegoPausado = false;
    }
    public void salir()
    {
        Application.Quit(); 
    }
}
