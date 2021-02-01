using UnityEngine;
using System.Collections;

public class StaticArea : MonoBehaviour
{
    GameObject playergo;
    Transform playerTr;
    PlayerMovement pm;
    bool staticReady = true;
    public float space = 0.3f;

	/// <summary>
	/// Obtiene el componente transform y el playermovement del gameobject player
	/// </summary>
    void Start()
    {
        playergo = GameObject.Find("Player");
        playerTr = playergo.GetComponent<Transform>();
        pm = playergo.GetComponent<PlayerMovement>();
    }

	/// <summary>
	/// Comprueba si el jugador se mueve y activa la plataforma si es necesario
	/// </summary
    void Update()
    {
        if (!pm.GetIfMovActive())
            StartCoroutine(setStaticReady());

        else staticReady = false;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
		/// <summary>
		/// Si el player se mantiene sobre la plataforma, este se para y se le permite elegir la dirección
		/// </summary>
        if (staticReady)
            if ((playerTr.position.x >= this.transform.position.x - space && playerTr.position.x <= this.transform.position.x + space) &&
                (playerTr.position.y >= this.transform.position.y - space && playerTr.position.y <= this.transform.position.y + space))
            {
                StartCoroutine(setPlayerPos());
            }
    }

	/// <summary>
	/// Se le permite el movimiento al player y se le establece la posicion de la plataforma
	/// </summary>
    IEnumerator setPlayerPos()
    {
        pm.SetDirectionNone();
        yield return new WaitForSeconds(.02f);
        pm.setMov(true);
        pm.setPos(this.transform.position);
    }

	/// <summary>
	/// Se activa de nuevo la plataforma
	/// </summary>
    IEnumerator setStaticReady()
    {
        yield return new WaitForSeconds(.05f);
        staticReady = true;
    }
}
