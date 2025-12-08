using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections.Generic;

public class TiendaArmasJugador : MonoBehaviour
{
    public GameObject tiendaDeArmas;
    private GameObject referenciaTienda;
    private bool estaActiva =false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            if (estaActiva == false)
            {
                referenciaTienda = Instantiate(tiendaDeArmas, new Vector2(0f, 0f), Quaternion.identity);
                referenciaTienda.SetActive(true);
                estaActiva=true;
            } else if(estaActiva == true)
            {
                Destroy(referenciaTienda);
                estaActiva=false;
            }
            
        
        }
    }
}
