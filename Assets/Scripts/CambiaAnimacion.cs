using UnityEngine;

public class CambiaAnimacion : MonoBehaviour
{
    private Animator animador;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private EstadoPersonaje estado;

    void Start()
    {
        animador = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        estado = GetComponentInChildren<EstadoPersonaje>();
    }

    void Update()
    {
        // Velocidad (Idle / Walk)
        float velocidadX = rb.linearVelocity.x;
        animador.SetFloat("velocidad", Mathf.Abs(velocidadX));

        // Voltear sprite (solo si se mueve)
        if (velocidadX != 0)
        {
            sr.flipX = velocidadX < 0;
        }

        // Estado de salto
        if (estado != null)
        {
            animador.SetBool("enPiso", estado.estaEnPiso);
        }
        else
        {
            // fallback por si algo está mal configurado
            animador.SetBool("enPiso", false);
        }
    }
}