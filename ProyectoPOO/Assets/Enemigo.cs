using UnityEngine;

public abstract class Enemigo : Entidad
{
    //----------------------------------------------------------Atributos del UML----------------------------------------------------------
    public float rangoDeteccion = 2f;
    protected bool jugadorDetectado;

    //----------------------------------------------------------Atributos de UNITY----------------------------------------------------------
    private Rigidbody2D rigidbody2D;
    public float velocidad = 1f;
    


    
    //----------------------------------------------------------Metodos de Unity----------------------------------------------------------
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        
    }
    //----------------------------------------------------------Metodos del UML----------------------------------------------------------
    public void movimientoAJugador(float xJugador)
    {
        if (xJugador > transform.position.x)
        {
            rigidbody2D.AddForce(new Vector2(velocidad, 0f), ForceMode2D.Impulse);
        }
        else
        {
            rigidbody2D.AddForce(new Vector2(-velocidad, 0f), ForceMode2D.Impulse);
        }
    }
    
}
