using UnityEngine;

public abstract class Enemigo : Entidad
{
    //----------------------------------------------------------Atributos del UML----------------------------------------------------------
    public float rangoDeteccion = 2f;
    protected bool jugadorDetectado; //no usado por transform

    //----------------------------------------------------------Atributos de UNITY----------------------------------------------------------
    
    public float velocidad = 1f;
    


    
    //----------------------------------------------------------Metodos de Unity----------------------------------------------------------
    void Start()
    {
        

        
    }
    //----------------------------------------------------------Metodos del UML----------------------------------------------------------
    public void movimientoAJugador(float xJugador)
    {
        if (xJugador > transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidad, 0f), ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-velocidad, 0f), ForceMode2D.Impulse);
        }
    }
    
    public abstract void ComportamientoPerseguir();
    public abstract void ComportamientoPreparar();
    public abstract void ComportamientoRetirada();
    public abstract void ComportamientoCooldown();
    
}
