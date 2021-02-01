using UnityEngine;
using System.Collections;

public class StartResolution : MonoBehaviour
{
    /// <summary>
    /// Alto de la pantalla
    /// </summary>
    int height;

    /// <summary>
    /// Ancho de la pantalla
    /// </summary>
    int width;

    /// <summary>
    /// Tamaño en píxeles original del juego
    /// </summary>
    int size = 64;

    /// <summary>
    /// Nueva resolución a la que se ajusta el juego (depende del tamaño de la pantalla)
    /// </summary>
    int newRes;

    /// <summary>
    /// Activa o desactiva la pantalla completa
    /// </summary>
    bool isF11Down = false;


    /// <summary>
    /// Obtiene las resoluciones de la pantalla donde se ejecuta el juego y calcula el tamaño inicial del juego
    /// </summary>
    void Start()
    {
        GetComponent<Camera>().orthographicSize = 6.4f;
        GetComponent<Camera>().backgroundColor = Color.black;

        Resolution[] res = Screen.resolutions; //obtengo resoluciones de la pantalla actual

        if (res[res.Length - 1].height >= 1000)
            height = res[res.Length - 4].height; //En pantallas de una buena resolución el juego será algo más pequeño que en otras pantallas

        else height = res[res.Length - 1].height;

        newRes = height / size;
        newRes *= size;

        Screen.SetResolution(newRes, newRes, false); //establezco la resolución calculada anteriormente

    }


    /// <summary>
    /// Controla el cambio de pantalla completa a modo ventana y viceversa
    /// </summary>
    void Update()
    {
		GetComponent<Camera>().backgroundColor = Color.black;
        if (Input.GetKeyDown(KeyCode.F11))
        {
            if (isF11Down)
            {
                Screen.SetResolution(newRes, newRes, false);
                isF11Down = false;
            }

            else
            {
                Screen.SetResolution(newRes, newRes, true);
                isF11Down = true;
            }
        }
    }

}