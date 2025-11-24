using UnityEngine;

public abstract class Entidad : MonoBehaviour
{
    protected int vidaMaxima;
    protected int vidaActual;
    protected int daño;
    protected bool estaVivo;

    public int getVidaMaxima()
    {
        return vidaMaxima;
    }
    public int getVidaActual()
    {
        return vidaActual;
    }

    public int getDaño()
    {
        return daño;
    }

    public void setVidaMaxima(int vidaMaxima)
    {
        this.vidaMaxima=vidaMaxima;
    }
    public void setVidaActual(int vidaActual)
    {
        this.vidaActual=vidaActual;
    }
    public void setDaño(int daño)
    {
        this.daño=daño;
    }
    public void aumentarDaño(int daño)
    {
        this.daño+=daño;
    }
    public abstract void atacar();
    public void recibirDaño(int daño)
    {
        vidaActual -= daño;
    }
    public abstract void morir();
}
