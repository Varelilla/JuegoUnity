using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemigo : MonoBehaviour
{
    public Transform objetivo;
    public GameObject prefabBala;
    public float velocidad = 3f;
    private Rigidbody2D rb;
    public int tipoEnemigo;
    public int vida;
    private float tiempoImpulso = 1f;
    private float tiempoParaDisparar = 2.0f;
    public float dragAereo;
    private SpriteRenderer spriteRenderer;
    private Canvas puntuacion;
    private Items itemSpawner;
    

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        puntuacion = GameObject.FindObjectOfType<Canvas>();
        itemSpawner = GameObject.Find("Spawns").GetComponent<Items>();
        rb = GetComponent<Rigidbody2D>();
        if (tipoEnemigo == 4)
            rb.drag = dragAereo;
        
    }

    // Update is called once per frame
    void Update()
    {
        tiempoParaDisparar -= Time.deltaTime;
        if (objetivo == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
                objetivo = player.transform;
        }
        else
        {
            switch (tipoEnemigo)
            {
                case 1:
                    SeguirObjetivo();
                    if (tiempoParaDisparar <= 0)
                    {
                        Disparar();
                        tiempoParaDisparar = 2.0f; // Restablece el temporizador
                    }
                    break;
                case 2:
                    SeguirObjetivo();
                    break;
                case 3:
                    MoverPorImpulsos();
                    break;
                case 4:
                    if (vida < 2)
                        velocidad = 4.5f; // Duplica la velocidad si la vida es menor a 2
                    SeguirObjetivo();
                    break;
            }
        }

        if (tipoEnemigo == 4 && vida >= 2)
            velocidad = 3f; // Restablece la velocidad si la vida es igual o mayor a 2
    }

    void SeguirObjetivo()
    {
        if (tipoEnemigo == 2 || tipoEnemigo == 4) {
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);
            if (transform.position.x < objetivo.position.x)
            {
                // Objetivo está a la derecha
                spriteRenderer.flipX = false;
            }
            else if (transform.position.x > objetivo.position.x)
            {
            // Objetivo está a la izquierda
            spriteRenderer.flipX = true;
            }
        } else {
            
            Vector2 direccion = objetivo.position - transform.position;
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angulo), 0.0125f).eulerAngles.z;
            direccion.Normalize();
            rb.velocity = transform.up * velocidad;
        }
        
    }

    void Disparar()
{
    GameObject bala = Instantiate(prefabBala, transform.position, Quaternion.Euler(0, 0, rb.rotation));
    Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();
    if (rbBala != null)
    {
        Vector2 direccion = (objetivo.position - transform.position).normalized;
        rbBala.AddForce(direccion * 300f); // Ajusta la fuerza según sea necesario
    }
    Destroy(bala, 3f);
}

    void MoverPorImpulsos()
{
    if (tiempoImpulso <= 0f)
    {
    
        Vector2 direccion = (objetivo.position - transform.position).normalized;
        rb.AddForce(direccion * velocidad*2, ForceMode2D.Impulse);
        tiempoImpulso = 2f; // Tiempo para el próximo impulso

        // Ajusta la velocidad horizontal después del impulso para frenar más rápido
        StartCoroutine(Frenar());
    }
    else
    {
        tiempoImpulso -= Time.deltaTime;
    }
}

    IEnumerator Frenar()
    {
        yield return new WaitForSeconds(1f); // Espera medio segundo antes de comenzar a frenar
        rb.velocity *= 0.2f; // Reduce la velocidad a la mitad (ajusta según necesidad)
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(col.gameObject);
        }
    }

    public void tocado()
    {
        vida--;
        if (vida <= 0)
        {

            switch (tipoEnemigo)
            {
                case 1:
                    puntuacion.GetComponent<Vida>().AddScore(10000);
                    break;
                case 2:
                    puntuacion.GetComponent<Vida>().AddScore(500);
                    break;
                case 3:
                    puntuacion.GetComponent<Vida>().AddScore(3000);
                    break;
                case 4:
                    puntuacion.GetComponent<Vida>().AddScore(1000);
                    break;
            }
            if (itemSpawner != null)
        {
            itemSpawner.itemAlMorir(transform.position);
        }
            Destroy(gameObject);
            
        }
    }
}
