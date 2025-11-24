using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections.Generic;

public class Jugador : Entidad
{
    //Atributos del UML
    private int stamina;
    private int nivel = 0;
    private int experiencia = 0;
    private int puntosDeHabilidad = 0;
    private int monedas = 0;
    private List<string> llaves = new List<string>();
    //private List<Arma> armasInventario =new List<Arma>();
    //private Arma armaActiva;
    private List<Habilidad> habilidades= new List<Habilidad>();

    //Atributos para funcionamiento en unity
    private Rigidbody2D rigidbody2D;
    public float velocidadHorizontal = 0.2f;
     public float fuerzaSalto = 5f;
    private bool espacioPresionado = false;
    public float velocidadMaxima = 2f;
    private bool exedioVelMax=false;
    private Vector2 velocidadActual;
    public Transform controladorSuelo; 
    public float radioSuelo = 0.2f;  
    public LayerMask queEsSuelo;     
    private bool enSuelo;
    private Animator animacion;

    //Metodos de Unity
    void Start()
    {
        animacion = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {   
        //Verificaciones para movimiento
        velocidadActual=rigidbody2D.linearVelocity;
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            espacioPresionado = true;
        }
        if (velocidadActual.x>velocidadMaxima||velocidadActual.x<-velocidadMaxima)
        {
            exedioVelMax=true;
        }
        else
        {
            exedioVelMax=false;
        }

        //Para animaciones
        animacion.SetFloat("velocidadX", Mathf.Abs(rigidbody2D.linearVelocity.x));
        if (rigidbody2D.linearVelocity.x > 0.1f) 
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // mirar derecha
        }
        else if (rigidbody2D.linearVelocity.x < -0.1f) 
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // mirar izquierda
        }
        animacion.SetFloat("velocidadY", rigidbody2D.linearVelocity.y);
        animacion.SetBool("enSuelo",enSuelo);
    }
    void FixedUpdate()
    {   
        //Movimiento en sí
        enSuelo = Physics2D.OverlapCircle(controladorSuelo.position, radioSuelo, queEsSuelo);
        mover();
    }

    private void OnDrawGizmos()
    {   
        //Para dibujar el detector del piso
        if (controladorSuelo != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(controladorSuelo.position, radioSuelo);
        }
    }

    //Metodos del UML
    public void mover()
    {
        if (Keyboard.current.dKey.isPressed && !exedioVelMax)
        {
            rigidbody2D.AddForce(new Vector2(velocidadHorizontal, 0f), ForceMode2D.Impulse);
        }
        if (Keyboard.current.aKey.isPressed && !exedioVelMax)
        {
            rigidbody2D.AddForce(new Vector2(-velocidadHorizontal, 0f), ForceMode2D.Impulse);
        }
        if(espacioPresionado && !enSuelo)
        {
            espacioPresionado = false;
        }
        if (espacioPresionado && enSuelo)
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

