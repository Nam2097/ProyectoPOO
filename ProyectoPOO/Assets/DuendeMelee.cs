using UnityEngine;

public class DuendeMelee : Enemigo
{
    [SerializeField] private float rangoAtaque = 1.5f;
    [SerializeField] private float cooldownAtaque = 1f;
    private float tiempoSiguienteAtaque;

    [SerializeField] private Transform puntoA;
    [SerializeField] private Transform puntoB;
    private Transform siguientePunto;

    protected override void Start()
    {
        base.Start();
        siguientePunto = puntoA;
    }

    public override void comportamiento()
    {
        if (detectarJugador())
        {
            float distancia = Vector2.Distance(transform.position, jugadorTarget.position);

            if (distancia <= rangoAtaque)
            {
                rb.linearVelocity = Vector2.zero;
                atacar();
            }
            else
            {
                movimientoAJugador();
            }
        }
        else
        {
            patrullar();
        }
    }

    private void patrullar()
    {
        if (puntoA == null || puntoB == null) return;

        transform.position = Vector2.MoveTowards(transform.position, siguientePunto.position, velocidadMovimiento * Time.deltaTime);

        if (Vector2.Distance(transform.position, siguientePunto.position) < 0.2f)
        {
            siguientePunto = (siguientePunto == puntoA) ? puntoB : puntoA;
        }

        float dir = siguientePunto.position.x > transform.position.x ? 1 : -1;
        transform.localScale = new Vector3(dir, 1, 1);
    }

    public override void atacar()
    {
        if (Time.time >= tiempoSiguienteAtaque)
        {
            if (animator != null) animator.SetTrigger("Attack");
            tiempoSiguienteAtaque = Time.time + cooldownAtaque;
        }
    }
}