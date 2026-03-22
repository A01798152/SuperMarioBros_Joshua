using UnityEngine;
using UnityEngine.InputSystem;

public class MueveMario : MonoBehaviour
{
    [SerializeField] private InputAction accionMover;
    [SerializeField] private InputAction accionSaltar;

    [SerializeField] private float velocidad = 4f;
    [SerializeField] private float fuerzaSalto = 5f;

    private Rigidbody2D rb;
    private Animator animador;

    private Vector2 inputMovimiento;
    private bool enSuelo;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
    }

    void OnEnable()
    {
        accionMover.Enable();
        accionSaltar.Enable();

        accionSaltar.performed += Saltar;
    }

    void OnDisable()
    {
        accionMover.Disable();
        accionSaltar.Disable();

        accionSaltar.performed -= Saltar;
    }

    void Update()
    {
        inputMovimiento = accionMover.ReadValue<Vector2>();

        // Animación de caminar/idle
        animador.SetFloat("velocidad", Mathf.Abs(inputMovimiento.x));

        // Voltear personaje
        if (inputMovimiento.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(inputMovimiento.x), 1, 1);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputMovimiento.x * velocidad, rb.linearVelocity.y);
    }

    void Saltar(InputAction.CallbackContext ctx)
    {
        if (enSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
            animador.SetBool("enSuelo", true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = false;
            animador.SetBool("enSuelo", false);
        }
    }
}