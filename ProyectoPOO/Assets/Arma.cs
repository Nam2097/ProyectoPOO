public class Arma
{
    // --- Atributos ---
    private int daño;
    private float velocidad;
    private string efecto;
    private string nombre;
    private int costo;
    private bool vendida;

    // --- Constructor ---
    public Arma(string nombre, int daño, float velocidad, string efecto, int costo)
    {
        this.nombre = nombre;
        this.daño = daño;
        this.velocidad = velocidad;
        this.efecto = efecto;
        this.costo = costo;
        this.vendida = false;
    }

    // --- Métodos ---

    public int GetDaño() => daño;
    public float GetVelocidad() => velocidad;
    public string GetNombre() => nombre;
    public int GetCosto() => costo;
    public bool EstaVendida() => vendida;

    public void MarcarComoVendida()
    {
        vendida = true;
    }
}