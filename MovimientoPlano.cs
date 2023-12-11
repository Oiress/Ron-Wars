using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlano : MonoBehaviour
{
    public float velocidad = 0.5f; // Velocidad de movimiento del plano en unidades por segundo
    public float distanciaMaxima = 1.0f; // Distancia máxima que se moverá el plano antes de regresar

    private Vector3 posicionInicial; // Posición inicial del plano
    private float distanciaRecorrida; // Distancia recorrida por el plano

    void Start()
    {
        posicionInicial = transform.position; // Guarda la posición inicial del plano al inicio
        distanciaRecorrida = 0.0f; // Inicializa la distancia recorrida en 0
    }

    void Update()
    {
        // Mueve el plano en la dirección positiva del eje X
        transform.Translate(Vector3.back * velocidad * Time.deltaTime);

        // Actualiza la distancia recorrida
        distanciaRecorrida += velocidad * Time.deltaTime;

        // Si la distancia recorrida es mayor o igual a la distancia máxima, regresa el plano a la posición inicial
        if (distanciaRecorrida >= distanciaMaxima)
        {
            transform.position = posicionInicial; // Regresa el plano a la posición inicial
            distanciaRecorrida = 0.0f; // Reinicia la distancia recorrida a 0
        }
    }
}
