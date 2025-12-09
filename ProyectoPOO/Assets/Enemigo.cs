using UnityEngine;

public abstract class Enemigo : Entidad
{
    [SerializeField] protected float rangoDeteccion;
    [SerializeField] protected int monedasDropear;
    protected bool jugadorDetectado;

    [SerializeField] protected float velocidadMovimiento = 3f;
    [SerializeField] protected float fuerzaSalto = 5f;

    [SerializeField] protected Transform detectoSuelo;
    [SerializeField] protected Transform detectoPared;
    [SerializeField] protected LayerMask capaSuelo;

    protected Transform jugadorTarget;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected bool tocandoSuelo;
    protected int direccionActual = 1;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null) jugadorTarget = obj.transform;
    }

    protected virtual void Update()
    {
        if (detectoSuelo != null)
            tocandoSuelo = Physics2D.OverlapCircle(detectoSuelo.position, 0.1f, capaSuelo);

        comportamiento();

        if (animator != null)
        {
            bool mov = Mathf.Abs(rb.linearVelocity.x) > 0.1f || Mathf.Abs(rb.linearVelocity.y) > 0.1f;
            animator.SetBool("IsRunning", mov);
        }
    }

    public bool detectarJugador()
    {
        if (jugadorTarget == null) return false;
        float distancia = Vector2.Distance(transform.position, jugadorTarget.position);
        jugadorDetectado = distancia <= rangoDeteccion;
        return jugadorDetectado;
    }

    public virtual void movimientoAJugador()
    {
        if (jugadorTarget == null) return;

        float dir = jugadorTarget.position.x > transform.position.x ? 1 : -1;
        direccionActual = (int)dir;

        rb.linearVelocity = new Vector2(dir * velocidadMovimiento, rb.linearVelocity.y);

        if (dir != 0)
            transform.localScale = new Vector3(dir, 1, 1);

        detectarYSaltar(dir);
    }

    protected void detectarYSaltar(float dir)
    {
        if (detectoPared == null) return;

        RaycastHit2D pared = Physics2D.Raycast(detectoPared.position, new Vector2(dir, 0), 0.5f, capaSuelo);

        if (pared.collider != null && tocandoSuelo)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
    }

    public int direccionMovimiento()
    {
        if (jugadorTarget == null) return 0;
        return jugadorTarget.position.x > transform.position.x ? 1 : -1;
    }

    public abstract void comportamiento();

    private void OnDrawGizmos()
    {
        if (detectoSuelo != null)
            Gizmos.DrawWireSphere(detectoSuelo.position, 0.1f);

        if (detectoPared != null)
            Gizmos.DrawLine(detectoPared.position, detectoPared.position + new Vector3(0.5f * transform.localScale.x, 0, 0));
    }
}

