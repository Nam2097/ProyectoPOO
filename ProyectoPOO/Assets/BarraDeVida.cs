using UnityEngine;
using UnityEngine.UI; 

public class BarraDeVida : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        if (slider == null) {
            slider = GetComponent<Slider>();
        }
    }

    // Esta función la llamará el Jugador cuando reciba daño
    public void CambiarVidaActual(int vidaActual, int vidaMaxima)
    {
        float porcentaje = (float)vidaActual / vidaMaxima;
        
        slider.value = porcentaje;
    }

    public void InicializarBarra(int vidaMaxima)
    {
        CambiarVidaActual(vidaMaxima, vidaMaxima);
    }
}