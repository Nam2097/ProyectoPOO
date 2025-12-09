using UnityEngine;
using UnityEngine.UI; 

public class BarraExperiencia : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        if (slider == null) {
            slider = GetComponent<Slider>();
        }
    }

    // Esta función la llamará el Jugador cuando reciba daño
    public void CambiarExperienciaActual(int experiencia)
    {
        float porcentaje = (float)experiencia / 100;
        
        slider.value = porcentaje;
    }
}