using UnityEngine;

public class AtaqueJugador : MonoBehaviour
{
    public int fuerzaGolpe = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            // Encajar con miguel para que reciban daño los enemigos
            /*Enemigo enemigoScript = collision.GetComponent<Enemigo>();

            
            if (enemigoScript != null)
            {
                enemigoScript.RecibirDaño(fuerzaGolpe);
            }*/
        }
    }
}
