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

    [SerializeField] private List<Arma> armasTienda = new List<Arma>();

    // Referencia al jugador real
    private Jugador jugador;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            jugador = FindObjectOfType<Jugador>();  //Encuentra al jugador automáticamente

            if (armasTienda == null)
                armasTienda = new List<Arma>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // ----------------- INICIALIZACIÓN DE ARMAS --------------------
    private void Start()
    {
    // Crear armas predeterminadas
    armasTienda = new List<Arma>()
    {
        new Arma("primera", 5, 1.5f, "Ninguno", 5),    // Arma ligera, barata
        new Arma("cargar", 20, 1.2f, "Carga", 12),        // Arma carga de ataque
        new Arma("hielo", 35, 0.9f, "Hielo", 25),      // Arma de hielo
        new Arma("veneno", 50, 0.7f, "Veneno", 40)   // Arma de veneno
    };

    Debug.Log("Armas creadas automáticamente en la tienda:");
    foreach (var arma in armasTienda)
    {
        Debug.Log($"Arma: {arma.GetNombre()} - Daño: {arma.GetDaño()} - Velocidad: {arma.GetVelocidad()} - Costo: {arma.GetCosto()} - Efecto: {arma.GetNombre()}");
    }
    }

    // ----------------- LÓGICA DE COMPRA --------------------

    public bool ComprarArma(string nombreArma)
    {
        Arma armaAComprar = armasTienda.Find(a =>
            a.GetNombre() == nombreArma && !a.EstaVendida());

        if (armaAComprar == null)
        {
            Debug.Log($"Arma '{nombreArma}' no existe o ya fue comprada.");
            return false;
        }

        int costo = armaAComprar.GetCosto();

        // 1. Verificar monedas usando Jugador
        int resultado = jugador.quitarMonedas(costo);  

        if (resultado == -1)
        {
            Debug.Log($"No tienes suficientes monedas. Necesitas {costo}.");
            return false;
        }

        // 2. Marcar como comprada
        armaAComprar.MarcarComoVendida();

        Debug.Log($"Compra exitosa: {nombreArma} por {costo} monedas.");

        return true;
    }

    // ---------- MÉTODO QUE SE USA DESDE UN BOTÓN DE UNITY ----------------

    public void ComprarArmaDesdeUI(string nombreArma)
    {
        bool ok = ComprarArma(nombreArma);

        if (ok)
            Debug.Log($"[UI] Compraste '{nombreArma}' correctamente.");
        else
            Debug.Log($"[UI] No pudiste comprar '{nombreArma}'.");
    }

    // ----------------- MÉTODOS EXTRA ---------------------

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