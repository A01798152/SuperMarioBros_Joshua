using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Regresa : MonoBehaviour
{
    private UIDocument menu;
    private Button botonRegresa;

    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;

        botonRegresa = root.Q<Button>("BotonRegresar");
        
        botonRegresa.clicked += RegresarNivel;
    }

    void OnDisable()
    {
        botonRegresa.clicked -= RegresarNivel;
    }

    void RegresarNivel()
    {
        SceneManager.LoadScene("Menu");
    }
}