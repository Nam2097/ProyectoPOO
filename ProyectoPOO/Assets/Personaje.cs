using UnityEngine;
using UnityEngine.InputSystem; 

public class Personaje : Entidad
{
    private int stamina;
    private int nivel;
    private int experiencia;
    private int puntosDeHabilidad;
    //private Arma arma;
    // private Habilidad[] habilidades;


    public Rigidbody2D rb;
    public float velocidadHorizontal = 1f;
    public float fuerzaSalto = 4f;
    public bool espacioPresionado = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            espacioPresionado = true;
        }
    }
    void FixedUpdate()
    {
        if (Keyboard.current.dKey.isPressed)
        {
            rb.AddForce(new Vector2(velocidadHorizontal, 0f), ForceMode2D.Impulse);
        }
        if (Keyboard.current.aKey.isPressed)
        {
            rb.AddForce(new Vector2(-velocidadHorizontal, 0f), ForceMode2D.Impulse);
        }
        if (espacioPresionado)
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            espacioPresionado = false;
        }
    }
    public override void atacar()
    {
        
    }
    public override void morir()
    {
        
    }
}

