using System.Collections.Generic;
using UnityEngine;

public class TiendaArmas : MonoBehaviour
{
    private static TiendaArmas instance;
    public static TiendaArmas Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TiendaArmas>();
                if (instance == null)
                {
                    GameObject go = new GameObject("TiendaArmas_Singleton");
                    instance = go.AddComponent<TiendaArmas>();
                }
            }
            return instance;
        }
    }

    // --- Atributos ---
    private List<Arma> armasTienda;

    // --- Constructor ---
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            armasTienda = new List<Arma>();
        }
        else if (instance != this)
        {
            Destroy(gameObject); 
        }
    }

    // --- Métodos ---

    public bool ComprarArma(string nombreArma, int monedasJugador)
    {
        Arma armaAComprar = armasTienda.Find(a => a.GetNombre() == nombreArma && !a.EstaVendida());

        if (armaAComprar == null)
        {
            Debug.Log($"Arma '{nombreArma}' no encontrada o ya está vendida.");
            return false;
        }

        int costo = armaAComprar.GetCosto();

        if (monedasJugador >= costo)
        {
            armaAComprar.MarcarComoVendida();
            Debug.Log($"Compra exitosa de '{nombreArma}' por {costo} monedas.");
            return true;
        }
        else
        {
            Debug.Log($"Monedas insuficientes para comprar '{nombreArma}'. Necesitas {costo}.");
            return false;
        }
    }

    public void AñadirArma(Arma arma)
    {
        if (arma != null)
        {
            armasTienda.Add(arma);
            Debug.Log($"Arma '{arma.GetNombre()}' añadida a la tienda.");
        }
    }

    public List<Arma> GetArmasDisponibles()
    {
        return armasTienda.FindAll(a => !a.EstaVendida());
    }
}