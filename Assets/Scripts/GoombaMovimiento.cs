using UnityEngine;

public class GoombaMovimiento : MonoBehaviour
{
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private float tiempoCambio = 6f;

    private float tiempo;
    private int direccion = -1;

    void Update()
    {
        transform.Translate(Vector2.right * direccion * velocidad * Time.deltaTime);

        tiempo += Time.deltaTime;
        if (tiempo >= tiempoCambio)
        {
            direccion *= -1;
            tiempo = 0f;
        }
    }
}