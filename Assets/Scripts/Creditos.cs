using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

// Script que controla la escena de créditos
// Hace que el texto suba automáticamente, desaparezca, espere un tiempo y vuelva a empezar
public class Creditos : MonoBehaviour
{
    // Referencias a elementos de la UI
    private Label _listaTexto;
    private VisualElement _contenedorScroll;
    private Button _botonVolver;

    // Velocidad a la que suben los créditos
    public float velocidadSubida = 60f;

    // Tiempo que la pantalla se queda "vacía" antes de reiniciar
    public float tiempoEnNegro = 2f;

    // Variables internas para controlar el movimiento
    private float _posicionY;
    private float _timerNegro = 0f;
    private bool _esperando = false;
    private bool _inicializado = false;

    // Se ejecuta cuando el objeto se activa
    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Se obtienen los elementos de la UI
        _listaTexto = root.Q<Label>("ListaTexto");
        _botonVolver = root.Q<Button>("BotonVolver");
        _contenedorScroll = root.Q<VisualElement>("ContenedorScroll");

        // Botón para regresar al menú
        if (_botonVolver != null)
            _botonVolver.clicked += () => SceneManager.LoadScene("Menu");

        // Este evento se usa para asegurarnos que Unity ya calculó el tamaño del texto
        _listaTexto.RegisterCallback<GeometryChangedEvent>(AlCargarGeometria);
    }

    // Se ejecuta cuando Unity ya conoce el tamaño real del texto
    void AlCargarGeometria(GeometryChangedEvent evt)
    {
        if (evt.newRect.height > 0)
        {
            ResetPosicion();
            _inicializado = true;

            // Se quita el callback para que no se repita innecesariamente
            _listaTexto.UnregisterCallback<GeometryChangedEvent>(AlCargarGeometria);
        }
    }

    // Reinicia la posición de los créditos
    void ResetPosicion()
    {
        // Coloca el texto abajo del contenedor (para que empiece desde abajo)
        _posicionY = _contenedorScroll.resolvedStyle.height;

        _esperando = false;
        _timerNegro = 0f;

        // Vuelve a mostrar el texto
        _listaTexto.style.opacity = 1;

        ActualizarTranslate();
    }

    // Se ejecuta cada frame
    void Update()
    {
        // Si no está listo o no hay texto, no hace nada
        if (!_inicializado || _listaTexto == null) return;

        // Mientras no esté en modo espera
        if (!_esperando)
        {
            // Movimiento hacia arriba
            _posicionY -= velocidadSubida * Time.deltaTime;
            ActualizarTranslate();

            // Cuando el texto ya salió completamente de pantalla
            if (_posicionY < -_listaTexto.resolvedStyle.height)
            {
                _esperando = true;

                // Ocultamos el texto para simular pantalla en negro
                _listaTexto.style.opacity = 0;
            }
        }
        else
        {
            // Contador de tiempo en negro
            _timerNegro += Time.deltaTime;

            // Cuando pasa el tiempo definido, reinicia los créditos
            if (_timerNegro >= tiempoEnNegro)
            {
                ResetPosicion();
            }
        }
    }

    // Aplica el movimiento al texto en pantalla
    void ActualizarTranslate()
    {
        _listaTexto.style.translate = new StyleTranslate(new Translate(0, _posicionY, 0));
    }
}