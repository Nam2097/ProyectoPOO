using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections.Generic;

public class Jugador : Entidad
{
    //----------------------------------------------------------Atributos del UML----------------------------------------------------------
    private int stamina;
    private int nivel = 0;
    private int experiencia = 0;
    private int puntosDeHabilidad = 0;
    private int monedas = 0;
    private List<string> llaves = new List<string>();
    //private List<Arma> armasInventario =new List<Arma>();
    //private Arma armaActiva;
    private List<Habilidad> habilidades= new List<Habilidad>();

    //----------------------------------------------------------Atributos para funcionamiento en unity----------------------------------------------------------
    //Movimiento
    private Rigidbody2D rigidbody2D;
    public float velocidadHorizontal = 0.2f;
     public float fuerzaSalto = 5f;
    private bool espacioPresionado = false;
    private int contadorSalto = 0;
    public float velocidadMaxima = 2f;
    private bool exedioVelMaxPositivo=false;
    private bool exedioVelMaxNegativo=false;
    private Vector2 velocidadActual;

    //Detector del suelo
    public Transform controladorSuelo; 
    public float radioSuelo = 0.2f;  
    public LayerMask queEsSuelo;     
    private bool enSuelo;
    
    //Para el ataque
    public bool atacando = false;
    public Collider2D hitboxCollider;
    private int buffPorSegundo =10;
    private float temporizadorAtaque = 0f;
    public float tiempoMaxCargado = 2f;
    private bool puedeAtacar = true;
    private bool cargandoAtaque=false;

    //Para animaciones
    private AnimatorStateInfo animacionActual;
    private Animator animacion;
    

    //-----------------------------------------------------------------Metodos de Unity----------------------------------------------------------
    void Start()
    {
        animacion = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        
        hitboxCollider.enabled = false;

        //Pruebas propias
        habilidades.Add(Habilidad.DOBLESALTO);
        habilidades.Add(Habilidad.CARGARATAQUE);
    }
    void Awake()
    {
        vidaMaxima=100;
        vidaActual=100;
        daño=20;
    }
    void Update()
    {   
        
        //Verificaciones para movimiento
        velocidadActual=rigidbody2D.linearVelocity; //obtener vector de velocidad
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !animacionActual.IsName("Atacar") && !cargandoAtaque) //espacio presionado o no pa saltar
        {
            espacioPresionado = true;
        }
        if (velocidadActual.x<-velocidadMaxima) //verificacion velocidad maxima hacia la izquierda
        {
            exedioVelMaxNegativo=true;
        }
        else
        {
            exedioVelMaxNegativo=false;
        }
        if (velocidadActual.x>velocidadMaxima) //verificacion velocidad maxima hacia la derecha
        {
            exedioVelMaxPositivo=true;
        }
        else
        {
            exedioVelMaxPositivo=false;
        }
        if(enSuelo && habilidades.Contains(Habilidad.DOBLESALTO)) //verificación de doble salto
        {
            contadorSalto =1;
        }
        //Ataque
        if(enSuelo && Keyboard.current.fKey.wasPressedThisFrame && puedeAtacar)
        {
            if (!habilidades.Contains(Habilidad.CARGARATAQUE))
            {
                atacar();
            }
            else
            {
                cargandoAtaque = true;
                temporizadorAtaque = 0f;
                rigidbody2D.linearVelocity = new Vector2(0f, 0f);
                cargarAtaque();
            }
            
        }
        if(enSuelo && Keyboard.current.fKey.isPressed && puedeAtacar)
        {
            if (habilidades.Contains(Habilidad.CARGARATAQUE))
            {
                cargarAtaque();
            }
            
        }
        if(enSuelo && Keyboard.current.fKey.wasReleasedThisFrame)
        {
            
            if (habilidades.Contains(Habilidad.CARGARATAQUE) && temporizadorAtaque!=0f)
            {
                atacar();
            }
        }
        if (atacando)
        {
            activarHitbox();
        }
        if (!atacando)
        {
            desactivarHitbox();
        }
        
        //Para animaciones
        animacionActual = animacion.GetCurrentAnimatorStateInfo(0);
        animacion.SetFloat("velocidadX", Mathf.Abs(rigidbody2D.linearVelocity.x));
        animacion.SetFloat("velocidadY", rigidbody2D.linearVelocity.y);
        animacion.SetBool("enSuelo",enSuelo);
        animacion.SetBool("cargarAtaque",cargandoAtaque);

        if (rigidbody2D.linearVelocity.x > 0.1f) 
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // mirar derecha
        }
        else if (rigidbody2D.linearVelocity.x < -0.1f) 
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // mirar izquierda
        }
        
        
    }
    void FixedUpdate()
    {   
        //Movimiento en sí
        enSuelo = Physics2D.OverlapCircle(controladorSuelo.position, radioSuelo, queEsSuelo); //Revisión si está en el suelo
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
    //para activar y desactivar la hitbox de daño
        public void activarHitbox()
    {
        if (hitboxCollider != null)
        {
            hitboxCollider.enabled = true;
        }
    }

    public void desactivarHitbox()
    {
        if (hitboxCollider != null)
        {
            hitboxCollider.enabled = false;
        }
    }
    public void cargarAtaque()
    {
        cargandoAtaque=true;
        temporizadorAtaque += Time.deltaTime;
        if (temporizadorAtaque > tiempoMaxCargado)
        {
            puedeAtacar=false;
            atacar();
        }
    }
    
    //-------------------------------------------------------------------Metodos del UML---------------------------------------------------------------------
    public void mover()
    {
        //para moverse a la izquierda o a la derecha

        if (Keyboard.current.dKey.isPressed && !exedioVelMaxPositivo && !animacionActual.IsName("Atacar") && !cargandoAtaque)
        {
            rigidbody2D.AddForce(new Vector2(velocidadHorizontal, 0f), ForceMode2D.Impulse);
        }
        if (Keyboard.current.aKey.isPressed && !exedioVelMaxNegativo && !animacionActual.IsName("Atacar") && !cargandoAtaque)
        {
            rigidbody2D.AddForce(new Vector2(-velocidadHorizontal, 0f), ForceMode2D.Impulse);
        }

        //para saltar
        if(espacioPresionado && !enSuelo && contadorSalto<=0)
        {   
            espacioPresionado = false;
        } else if (espacioPresionado && !enSuelo && contadorSalto>=1) //doble salto
        {
            if (contadorSalto >= 1)
            {
                contadorSalto--;
            }
            espacioPresionado = false;
            rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, 0f);
            rigidbody2D.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }
        if (espacioPresionado && enSuelo) //salto normal
        {
            rigidbody2D.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            espacioPresionado = false;
        }

    }
    public override void atacar()
    {
        if (habilidades.Contains(Habilidad.CARGARATAQUE))
        {
            cargandoAtaque=false;
            rigidbody2D.linearVelocity = new Vector2(0f, 0f);
            int dañoTemporal = daño;
            daño += (int)temporizadorAtaque*buffPorSegundo;
            puedeAtacar=true;
        }
        else
        {
            rigidbody2D.linearVelocity = new Vector2(0f, 0f);
            animacion.SetTrigger("Ataque");
        }
        
            
    }
    public override void morir()
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

