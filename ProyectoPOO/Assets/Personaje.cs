using UnityEngine;
using UnityEngine.InputSystem; 

public class Personaje : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
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
}