using UnityEngine;

public class AtaqueFly : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int daño;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Jugador jugadorScript = collision.GetComponent<Jugador>();

            
            if (jugadorScript != null)
            {
                daño= GetComponentInParent<Enemigo>().getDaño();
                jugadorScript.recibirDaño(daño);
            }
        }
    }
}
