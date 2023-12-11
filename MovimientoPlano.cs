using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlano : MonoBehaviour
{
    public float velocidad = 0.5f; // Velocidad de movimiento del plano en unidades por segundo
    public float distanciaMaxima = 1.0f; // Distancia m�xima que se mover� el plano antes de regresar

    private Vector3 posicionInicial; // Posici�n inicial del plano
    private float distanciaRecorrida; // Distancia recorrida por el plano

    void Start()
    {
        posicionInicial = transform.position; // Guarda la posici�n inicial del plano al inicio
        distanciaRecorrida = 0.0f; // Inicializa la distancia recorrida en 0
    }

    void Update()
    {
        // Mueve el plano en la direcci�n positiva del eje X
        transform.Translate(Vector3.back * velocidad * Time.deltaTime);

        // Actualiza la distancia recorrida
        distanciaRecorrida += velocidad * Time.deltaTime;

        // Si la distancia recorrida es mayor o igual a la distancia m�xima, regresa el plano a la posici�n inicial
        if (distanciaRecorrida >= distanciaMaxima)
        {
            transform.position = posicionInicial; // Regresa el plano a la posici�n inicial
            distanciaRecorrida = 0.0f; // Reinicia la distancia recorrida a 0
        }
    }
}
