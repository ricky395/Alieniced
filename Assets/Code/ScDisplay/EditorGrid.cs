using UnityEngine;

[ExecuteInEditMode]
public class EditorGrid : MonoBehaviour
{
    /// <summary>
    /// Tamaño del recuadro de la malla
    /// </summary>
    public float cell_size = 0.2f;

    /// <summary>
    /// Posiciones en el espacio del objeto
    /// </summary>
    private float x, y, z;


    /// <summary>
    /// Reset de la posición del objeto
    /// </summary>
    void Start()
    {
        x = 0f;
        y = 0f;
        z = 0f;
    }


    /// <summary>
    /// Calcula la posición del objeto y la redondea a la casilla más cercana.
    /// </summary>
    void Update()
    {
        x = Mathf.Round(transform.position.x / cell_size) * cell_size;
        y = Mathf.Round(transform.position.y / cell_size) * cell_size;
        z = transform.position.z;
        transform.position = new Vector3(x, y, z);
    }
    
}