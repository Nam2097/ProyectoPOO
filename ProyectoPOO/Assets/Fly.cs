using UnityEngine;

public class Fly : Enemigo
{
    //----------------------------------------------------------Atributos del UML----------------------------------------------------------

    //----------------------------------------------------------Atributos de UNITY----------------------------------------------------------
    private Transform objetivoActual;
    private SpriteRenderer spriteRenderer;
    //para el ataque
    public float rangoAtaque = 0.5f; 
    public float tiempoPreparacion = 0.5f; 
    public float tiempoRetirada = 1.0f; 
    public float cooldownAtaque = 2.0f;
    private bool estaOcupado = false; 
    private bool puedeAtacar = true;
    public GameObject hitboxAtaque;
    public bool atacando = false;
    //comportamiento
    private Estado estadoActual = Estado.PERSIGUIENDO;
    private float temporizador = 0f;
    //ANIMACIONES
    private AnimatorStateInfo animacionActual;
    private Animator animacion;
    //muerte
    public bool murio = false;
    public bool inicioMuerte=false;

    public int dañoPrueba=0;

    

    //----------------------------------------------------------Metodos de Unity----------------------------------------------------------
    void Start()
    {
        if (dañoPrueba == 0)
        {
            dañoPrueba= getDaño();
        }
        animacion = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        setVidaMaxima(50); 
        setVidaActual(50);
        setDaño(dañoPrueba);  
        velocidad = 1f;
        hitboxAtaque.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (vidaActual <= 0 && !inicioMuerte)
        {
            animacion.SetTrigger("morir");
            inicioMuerte=true;
        }
        if (murio)
        {
            morir();
        }
        if (atacando)
        {
            hitboxAtaque.SetActive(true);
        }
        if (!atacando)
        {
            hitboxAtaque.SetActive(false);
        }
    }
    void FixedUpdate()
    {
        if (objetivoActual == null) return;

        // Máquina de Estados: ¿Qué estoy haciendo ahora?
        switch (estadoActual)
        {
            case Estado.PERSIGUIENDO:
                ComportamientoPerseguir();
                break;

            case Estado.PREPARANDO:
                ComportamientoPreparar();
                break;

            case Estado.RETIRADA:
                ComportamientoRetirada();
                break;

            case Estado.COOLDOWN:
                ComportamientoCooldown();
                break;
        }
    }
    public void EstablecerObjetivo(Transform nuevoObjetivo)
    {
        objetivoActual = nuevoObjetivo;
        if (estadoActual == Estado.COOLDOWN) 
            estadoActual = Estado.PERSIGUIENDO;
    }

    public void PerderObjetivo()
    {
        objetivoActual = null;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; 
        estadoActual = Estado.PERSIGUIENDO;
    }
    //----------------------------------------------------------Metodos del UML----------------------------------------------------------
    public override void morir()
    {
        Debug.Log("La mosca ha muerto");

        Destroy(gameObject);
    }
    public override void atacar()
    {
        animacion.SetTrigger("atacar");
    }
    public void movimientoAJugador()
    {
        Vector2 direccion = (objetivoActual.position - transform.position).normalized;

        GetComponent<Rigidbody2D>().linearVelocity = direccion * velocidad;

        if (direccion.x > 0) spriteRenderer.flipX = true; //mirar a la derecha
        else if (direccion.x < 0) spriteRenderer.flipX = false; //mirar a la izquierda
    }
    
    public override void ComportamientoPerseguir()
    {
        float distancia = Vector2.Distance(transform.position, objetivoActual.position);

        if (distancia > rangoAtaque) //aun no está a rango de ataque
        {
            // usamos nuestro propio movimiento, no la del padre pq solo es en x
            movimientoAJugador();
        }
        else //está a rango de ataque
        {
            estadoActual = Estado.PREPARANDO;
            temporizador = tiempoPreparacion;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; 
        }
    }
    public override void ComportamientoPreparar()
    {
        temporizador -= Time.deltaTime;


        if (temporizador <= 0)
        {
            atacar();
            estadoActual = Estado.RETIRADA;
            temporizador = tiempoRetirada;
        }
    }
    public override void ComportamientoRetirada()
    {
        Vector2 direccionHuida = (transform.position - objetivoActual.position).normalized;
        GetComponent<Rigidbody2D>().linearVelocity = direccionHuida * (velocidad * 1.5f);


        temporizador -= Time.deltaTime;

        if (temporizador <= 0)
        {
            estadoActual = Estado.COOLDOWN;
            temporizador = cooldownAtaque;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
    public override void ComportamientoCooldown()
    {
        temporizador -= Time.deltaTime;

        if (temporizador <= 0)
        {
            estadoActual = Estado.PERSIGUIENDO;
        }
    }
}
