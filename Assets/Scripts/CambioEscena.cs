using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    public void Cambio(string nombre)
    {
        SceneManager.LoadScene(nombre);
        Time.timeScale = 1f;
    }

    public void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
                                        Application.Quit();
#endif
    }
}