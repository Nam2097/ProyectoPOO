using UnityEngine;

public class ControladorCamara : MonoBehaviour
{
    public int dondeEsta=1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cambiarSitio(int numSitio) //comienza en 1
    {
        transform.position = new Vector3((float)(numSitio-1)*10f,0f,-10f);
        dondeEsta=numSitio;
    }
}
