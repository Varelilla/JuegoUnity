using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D cuerpo;
    public Vector2 movimiento;
    Vector2 mousePos;
    public Camera camara;
    public int vida = 10;
    private bool inmune = false;
    private float tiempoInmunidad = 0;
    private SpriteRenderer spriteRenderer;
    Animator anim;
    public Canvas vidaCanvas;
    public int vidaMaxima = 5;
    public float velMaxima = 10f;
    private Balas balas;
    public PantallaDerrota pantallaMuerte;


    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();  // Asegúrate de tener un SpriteRenderer en el mismo GameObject
        balas = GetComponent<Balas>();
    }

    void Update()
    {
        movimiento.x = Input.GetAxisRaw("Horizontal");
        movimiento.y = Input.GetAxisRaw("Vertical");

        mousePos = camara.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mirarPos = mousePos - cuerpo.position;
        float angulo = Mathf.Atan2(mirarPos.y, mirarPos.x) * Mathf.Rad2Deg;
        cuerpo.rotation = angulo;

        if (tiempoInmunidad > 0)
        {
            tiempoInmunidad -= Time.deltaTime;
            spriteRenderer.enabled = Mathf.FloorToInt(tiempoInmunidad * 10) % 2 == 0;
        }
        else
        {
            inmune = false;
            spriteRenderer.enabled = true;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    void FixedUpdate()
    {
        cuerpo.MovePosition(cuerpo.position + movimiento * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!inmune && (col.gameObject.tag == "Enemigo" || col.gameObject.tag == "balaEnemigo"))
        {
            
            vida--;
            vidaCanvas.GetComponent<Vida>().actualizarVida(vida);
            if (vida <= 0)
            {
                pantallaMuerte.Setup(vidaCanvas.GetComponent<Vida>().score);
            }
            else
            {
                inmune = true;
                tiempoInmunidad = 3f;
                gameObject.layer = LayerMask.NameToLayer("BalasEnemigo");  // Cambia al layer "Inmune" donde no hay colisiones
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("vidaItem"))
        {
            vida = Mathf.Min(vidaMaxima, vida + 1);
            vidaCanvas.GetComponent<Vida>().actualizarVida(vida);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("velocidadItem"))
        {
            speed = Mathf.Min(velMaxima, speed + 2);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("balaItem"))
        {
            balas.aumentarBalas(1);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("cadenciaItem"))
        {
            balas.fireRate(-0.055555f);
            Destroy(other.gameObject);
        }
    }
}
