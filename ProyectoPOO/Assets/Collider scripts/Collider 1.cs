using UnityEngine;

public class Collider1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject camara;
    private ControladorCamara controladorCamara;
    void Start()
    {
        controladorCamara= camara.GetComponent<ControladorCamara>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    if (collision.gameObject.CompareTag("Player")) 
    {
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                controladorCamara.cambiarSitio(controladorCamara.dondeEsta+1);
            }
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                controladorCamara.cambiarSitio(controladorCamara.dondeEsta-1);
            }
    }
    }
}
