using UnityEngine;

public class DeteccionFly : MonoBehaviour
{
    private Fly scriptFly;

    void Start()
    {
        scriptFly = GetComponentInParent<Fly>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            scriptFly.EstablecerObjetivo(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            scriptFly.PerderObjetivo();
        }
    }
}
