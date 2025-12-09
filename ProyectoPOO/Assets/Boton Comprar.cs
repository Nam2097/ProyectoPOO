using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections.Generic;

public class BotonComprar : MonoBehaviour
{
    private SpriteRenderer renderizador;
    private Color colorOriginal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        renderizador = GetComponent<SpriteRenderer>();
        colorOriginal = renderizador.color;
    }

    // Update is called once per frame
    void Update()
    {
        renderizador = GetComponent<SpriteRenderer>();
    }
    void OnMouseEnter()
    {
        // El mouse acaba de entrar: resalta el objeto.
        renderizador.color = Color.yellow; 
    }

    void OnMouseExit()
    {
        // El mouse acaba de salir: regresa al color original.
        renderizador.color = colorOriginal;
    }
}
