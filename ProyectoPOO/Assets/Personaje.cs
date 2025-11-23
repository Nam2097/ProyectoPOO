using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections.Generic;

public class Personaje : Entidad
{
    private int stamina;
    private int nivel = 0;
    private int experiencia = 0;
    private int puntosDeHabilidad = 0;
    private int monedas = 0;
    private List<string> llaves = new List<string>();
    //private List<Arma> armasInventario =new List<Arma>();
    //private Arma armaActiva;
    private List<Habilidad> habilidades= new List<Habilidad>();


    public Rigidbody2D rigidbody2D;
    public float velocidadHorizontal = 1f;
     public float fuerzaSalto = 4f;
    private bool espacioPresionado = false;
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
        mover();
    }
    public void mover()
    {
        
        if (Keyboard.current.dKey.isPressed)
        {
            rigidbody2D.AddForce(new Vector2(velocidadHorizontal, 0f), ForceMode2D.Impulse);
        }
        if (Keyboard.current.aKey.isPressed)
        {
            rigidbody2D.AddForce(new Vector2(-velocidadHorizontal, 0f), ForceMode2D.Impulse);
        }
        if (espacioPresionado)
        {
            rigidbody2D.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            espacioPresionado = false;
        }

    }
    public override void atacar()
    {
        
    }
    public void añadirHabilidad(Habilidad habilidad)
    {
        habilidades.Add(habilidad);
    }
    /*public void añadirArmaInventario(Arma arma)
    {
        armasInventario.add(arma);
    }*/
    /*public void cambiarArmaActiva(Arma arma)
    {
        armaActiva = arma;
    }*/
    public void aumentarExperiencia(int experiencia)
    {
        if (this.experiencia + experiencia >= 100)
        {
            nivel++;
            this.experiencia = this.experiencia + experiencia -100;
        }
        else
        {
           this.experiencia+= experiencia; 
        }
        
    }
    public void añadirMonedas(int monedas)
    {
        this.monedas+=monedas;
    }
    public int quitarMonedas(int monedas)
    {
        if (this.monedas - monedas < 0)
        {
            return -1; //no le alcanzaron las monedas
        }
        else
        {
            this.monedas-=monedas;
            return 0;
        }
    }

}

