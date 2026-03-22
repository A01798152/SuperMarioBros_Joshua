using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private UIDocument menu;
    
    // Contenedor de botones (para ocultarlos cuando abres una ventana)
    private VisualElement contenedorBotones;

    // --- Botones principales ---
    private Button botonJugar;
    private Button botonAyuda;
    private Button botonCreditos;
    private Button botonSalir; 

    // --- Elementos de Créditos ---
    private VisualElement ventanaCreditos;
    private VisualElement mascaraCreditos; // ¡NUEVO! Para medir el tamaño de la ventana
    private VisualElement textoCreditos;
    private Button botonCerrarCreditos;
    
    public float velocidadScroll = 50f;
    private float posicionYActual;
    private bool animandoCreditos = false;

    // --- Elementos de Ayuda ---
    private VisualElement ventanaAyuda;
    private Button botonCerrarAyuda;

    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;

        // Buscamos el contenedor de tus botones
        contenedorBotones = root.Q<VisualElement>("Botones");

        // 1. Botones principales
        botonJugar = root.Q<Button>("boton-juego"); 
        botonAyuda = root.Q<Button>("boton-ayuda");
        botonCreditos = root.Q<Button>("boton-creditos");
        botonSalir = root.Q<Button>("boton-salir"); 
        
        // 2. Elementos de Créditos
        ventanaCreditos = root.Q<VisualElement>("Creditos");
        mascaraCreditos = root.Q<VisualElement>("MascaraCreditos"); // Lo buscamos para la matemática
        textoCreditos = root.Q<VisualElement>("TextoCreditos");
        botonCerrarCreditos = root.Q<Button>("CerrarCreditos");

        // 3. Elementos de Ayuda
        ventanaAyuda = root.Q<VisualElement>("Ayuda");
        botonCerrarAyuda = root.Q<Button>("CerrarAyuda");

        // 4. Conectamos todo
        if(botonJugar != null) botonJugar.clicked += IniciarJuego;
        if(botonSalir != null) botonSalir.clicked += CerrarJuego; 
        
        if(botonCreditos != null) botonCreditos.clicked += MostrarCreditos;
        if(botonCerrarCreditos != null) botonCerrarCreditos.clicked += OcultarCreditos;

        if(botonAyuda != null) botonAyuda.clicked += MostrarAyuda;
        if(botonCerrarAyuda != null) botonCerrarAyuda.clicked += OcultarAyuda;
    }

    void Update()
    {
        // El nuevo motor de Star Wars súper inteligente
        if (animandoCreditos && textoCreditos != null && mascaraCreditos != null)
        {
            posicionYActual -= velocidadScroll * Time.deltaTime;
            textoCreditos.style.translate = new Translate(0, posicionYActual, 0);

            // Calculamos cuánto mide tu texto y cuánto mide la máscara
            float alturaTexto = textoCreditos.layout.height;
            float alturaMascara = mascaraCreditos.layout.height;

            // Si el texto ya subió por completo...
            if (Mathf.Abs(posicionYActual) > alturaTexto)
            {
                // Lo reiniciamos exactamente en el borde de abajo
                posicionYActual = alturaMascara; 
            }
        }
    }

    private void IniciarJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void CerrarJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Esto funciona en el juego exportado
        
        // El truco de tu amigo para el Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // --- Lógica de Ayuda ---
    private void MostrarAyuda()
    {
        if(contenedorBotones != null) contenedorBotones.style.display = DisplayStyle.None; // Oculta menú principal
        if(ventanaAyuda != null) ventanaAyuda.style.display = DisplayStyle.Flex; // Muestra ayuda
    }

    private void OcultarAyuda()
    {
        if(ventanaAyuda != null) ventanaAyuda.style.display = DisplayStyle.None;
        if(contenedorBotones != null) contenedorBotones.style.display = DisplayStyle.Flex; // Regresa el menú
    }

    // --- Lógica de Créditos ---
    private void MostrarCreditos()
    {
        if(contenedorBotones != null) contenedorBotones.style.display = DisplayStyle.None;
        
        if(ventanaCreditos != null)
        {
            ventanaCreditos.style.display = DisplayStyle.Flex;
            posicionYActual = 0f; // Reiniciamos a 0 para que la matemática no falle
            animandoCreditos = true; 
        }
    }

    private void OcultarCreditos()
    {
        if(ventanaCreditos != null)
        {
            ventanaCreditos.style.display = DisplayStyle.None;
            animandoCreditos = false; 
        }
        if(contenedorBotones != null) contenedorBotones.style.display = DisplayStyle.Flex;
    }
}