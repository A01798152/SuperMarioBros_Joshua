using UnityEngine;

public class EstadoPersonaje : MonoBehaviour
{
    public bool estaEnPiso { get; private set; }

    private int contactosSuelo = 0;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Suelo"))
        {
            contactosSuelo++;
            estaEnPiso = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Suelo"))
        {
            contactosSuelo--;

            if (contactosSuelo <= 0)
            {
                contactosSuelo = 0;
                estaEnPiso = false;
            }
        }
    }
}