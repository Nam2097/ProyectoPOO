using UnityEngine;
using UnityEngine.InputSystem; 
// using System.Collections.Generic; // No la necesitas aquí si no usas Listas

public class TiendaArmasJugador : MonoBehaviour
{
    [Header("Arrastra aquí el objeto VentanaTienda del Canvas")]
    public GameObject tiendaDeArmas; // Ahora esto será el objeto de la escena, no el prefab

    void Start()
    {
        // Opcional: Nos aseguramos de que la tienda empiece cerrada al iniciar el juego
        if (tiendaDeArmas != null)
        {
            tiendaDeArmas.SetActive(false);
        }
    }

    void Update()
    {
        // Al presionar P
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            if (tiendaDeArmas != null)
            {
                // 1. Revisamos si está activa actualmente
                bool estaActiva = tiendaDeArmas.activeSelf;

                // 2. Le damos la vuelta (Si es true -> false, Si es false -> true)
                tiendaDeArmas.SetActive(!estaActiva);
                
                // Debug opcional para ver que funciona
                if (!estaActiva) Debug.Log("Tienda Abierta");
                else Debug.Log("Tienda Cerrada");
            }
            else
            {
                Debug.LogWarning("¡Falta asignar la Tienda en el Inspector!");
            }
        }
    }
}