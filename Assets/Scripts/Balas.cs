using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balas : MonoBehaviour
{
    public Transform objetivo;
    public GameObject prefabBala;
    public float velocidad = 20f;
    public float cadenciaDeDisparo = 0.3333f;  // 3 balas por segundo, 1/3 de segundo entre balas
    private float tiempoProximoDisparo = 0f;  // Controla cuándo será el próximo disparo
    public float maxCadencia = 0.16666f;     
    public int balasMaximas = 3;
    public int balas = 1;

    void Update()
    {
        // Comprueba si el botón está siendo presionado y si el tiempo actual es mayor que el tiempo del próximo disparo
        if (Input.GetButton("Fire1") && Time.time >= tiempoProximoDisparo)
        {
            tiempoProximoDisparo = Time.time + cadenciaDeDisparo;  // Calcula el próximo tiempo de disparo
            Disparar();
        }
    }

    void Disparar()
    {
        for (int i = 0; i < balas; i++)
        {
            GameObject bala = Instantiate(prefabBala, objetivo.position, objetivo.rotation);
            Quaternion ajusteRotacion = Quaternion.Euler(0, 0, Random.Range(0, 5));
            Vector2 direccionAjustada = ajusteRotacion * objetivo.transform.right;
            bala.transform.Rotate(0, 0, -90);
            Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();
            rb.AddForce(direccionAjustada * velocidad, ForceMode2D.Impulse);
        }
        
    }

    public void aumentarBalas(int a)
    {
        balas = Mathf.Min(balas + a, balasMaximas);
    }

    public void fireRate(float d)
    {
        cadenciaDeDisparo = Mathf.Max(cadenciaDeDisparo + d, maxCadencia);
    }
}
