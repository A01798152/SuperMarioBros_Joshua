using UnityEngine;
using UnityEngine.InputSystem;

public class MueveMario : MonoBehaviour
{
    [SerializeField] private InputAction accionMover;
    [SerializeField] private InputAction accionSaltar;

    [SerializeField] private float velocidad = 7f;
    [SerializeField] private float fuerzaSalto = 10f;

    private Rigidbody2D rb;
    private Vector2 inputMovimiento;
    private bool enSuelo;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = false;
        }
    }
}