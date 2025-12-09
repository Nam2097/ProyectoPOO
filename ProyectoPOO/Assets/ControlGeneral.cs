using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlGeneral : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void comenzarJuego()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void salir()
    {
        Application.Quit(); 
    }
}
