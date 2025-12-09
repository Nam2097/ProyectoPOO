using UnityEngine;

public class AtaqueJugador : MonoBehaviour
{
    public int daño;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            Enemigo enemigoScript = collision.GetComponent<Enemigo>();

            
            if (enemigoScript != null)
            {
                daño= GetComponentInParent<Jugador>().getDaño();
                enemigoScript.recibirDaño(daño);
            }
        }
    }
}
