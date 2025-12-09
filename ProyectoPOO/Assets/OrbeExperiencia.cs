using UnityEngine;

public class OrbeExperiencia : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Jugador jugador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jugador = FindObjectOfType<Jugador>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    if (collision.gameObject.CompareTag("Player")) 
    {
            jugador.aumentarExperiencia(20);
            Destroy(gameObject);
    }
    }
}
