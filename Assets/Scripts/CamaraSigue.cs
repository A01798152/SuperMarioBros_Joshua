using UnityEngine;

public class CamaraSigue : MonoBehaviour
{
    [SerializeField] private Transform objetivo;

    [Header("Suavizado")]
    [SerializeField] private float suavizado = 0.1f;

    [Header("Offset")]
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    [Header("Limites del mapa")]
    [SerializeField] private float limiteIzquierdo;
    [SerializeField] private float limiteDerecho;

    void LateUpdate()
    {
        if (objetivo == null) return;

        // Solo sigue en X (estilo Mario)
        float posicionX = Mathf.Lerp(transform.position.x, objetivo.position.x, suavizado);

        // Limitar dentro del mapa
        posicionX = Mathf.Clamp(posicionX, limiteIzquierdo, limiteDerecho);

        // Mantener Y fijo (no sigue el salto)
        transform.position = new Vector3(posicionX, transform.position.y, offset.z);
    }
}