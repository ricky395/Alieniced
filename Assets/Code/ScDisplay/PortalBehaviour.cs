using UnityEngine;
using System.Collections;

public class PortalBehaviour : MonoBehaviour
{
    /// <summary>
    /// Componente playermovement del player
    /// </summary>
    PlayerMovement pm;
    /// <summary>
    /// Componente transform del player
    /// </summary>
    Transform playerTr;
    /// <summary>
    /// Componente transform de los portales
    /// </summary
    Transform[] portals;
    /// <summary>
    /// Espacio
    /// </summary
    float space = 0.3f;
    /// <summary>
    /// Indica si el portal esta activo
    /// </summary
    bool activePortal = true;
    /// <summary>
    /// Indica si el jugador esta sobre un portal
    /// </summary
    bool isOnPortal = false;
    /// <summary>
    /// Componente del controlador del audio
    /// </summary
    AudioController ac;
    ShowLines lines;

    /// <summary>
    /// Obtiene componentes y se los asigna a las variables anteriormente declaradas
    /// </summary
    void Start()
    {
        InitPortals();
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerTr = GameObject.Find("Player").GetComponent<Transform>();
        ac = GameObject.Find("SceneManager(Clone)").GetComponent<AudioController>();
    }

    void Update()
    {
        //ShowLines.ShowPortals(portals);
        /// <summary>
        /// Si el portal esta activo se compara la posición y se le teletranporta al player si se requiere
        /// </summary
        if (activePortal)
        {
            if (ComparePosition(1))
            {
                ac.PlayPortal();
                pm.setPos(portals[2].position);
                StartCoroutine(setPortalActive(2));
            }

            else if (ComparePosition(2))
            {
                ac.PlayPortal();
                pm.setPos(portals[1].position);
                StartCoroutine(setPortalActive(1));
            }
        }

        if (isOnPortal)
        {
            activePortal = false;

            if (ComparePosition(1) == ComparePosition(2))
            {
                isOnPortal = false;
                activePortal = true;
            }
        }
    }

    /// <summary>
    /// Maneja la activacion de los portales y si el jugdor esta sobre ellos
    /// </summary
    public IEnumerator setPortalActive(int arrayIndex)
    {
        activePortal = false;
        yield return new WaitForSeconds(0.2f);

        if (!((playerTr.position.x >= portals[arrayIndex].transform.position.x - space && playerTr.position.x <= portals[arrayIndex].transform.position.x + space) &&
            (playerTr.position.y >= portals[arrayIndex].transform.position.y - space && playerTr.position.y <= portals[arrayIndex].transform.position.y + space)))
            activePortal = true;
        else
            isOnPortal = true;
    }

    /// <summary>
    /// Se le pasa una posicion y devuelve true si esta sobre algún portal o la meta
    /// </summary
    bool ComparePosition(int index)
    {
        return (playerTr.position.x >= portals[index].transform.position.x - space && playerTr.position.x <= portals[index].transform.position.x + space) &&
                (playerTr.position.y >= portals[index].transform.position.y - space && playerTr.position.y <= portals[index].transform.position.y + space);
    }

    void InitPortals()
    {
        portals = GetComponentsInChildren<Transform>(); //3 posiciones. 1 y 2 son los portales
    }

    public void OnDrawGizmos()
    {
        if (portals == null)
            InitPortals();
        if (lines == null)
            lines = GetComponent<ShowLines>();

        lines.Show(portals);
    }
}