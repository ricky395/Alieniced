using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{

    /// <summary>
    /// Dirección del jugador
    /// </summary>
    enum Direction { none, upMov, rightMov, downMov, leftMov };
    Direction playerDir;
    Direction lastDirLR;
    Direction lastDirUD;

    Transform tr;

    /// <summary>
    /// Distancia mínima que recorre el personaje en cada movimiento por frame
    /// </summary>
    float space = 0.2f;
    [Tooltip("Velocidad del enemigo. Manejarla junto con 'repeat rate' para que el enemigo no se vuelva a mover antes de colisionar")]
    [Range(0.3f, 3f)]
    public float velocity = 1;

    [Tooltip("Cada cuánto tiempo se mueve el enemigo")]
    /// <summary>
    /// Cada cuánto tiempo se mueve el enemigo
    /// </summary>
    public float repeatRate = 3;

    [Tooltip("Activado: el enemigo se mueve de izquierda a derecha (No compatible con 'up down')")]
    public bool leftRight = true;

    [Tooltip("Activado: el enemigo se mueve de arriba a abajo (No compatible con 'left right')")]
    public bool upDown = false;
    Vector3 direction;

    /// <summary>
    /// Cada cuanto tiempo hace el jugador un movimiento (velocidad del cambio de posición)
    /// </summary>
    float timeToGo = 0.03f;

    /// <summary>
    /// Cuenta que junto con timeToGo establecen la velocidad de movimiento
    /// </summary>
    float count = 0;
    Animator anim;
    EditorGrid gridAlign;

    float cell_size = 0.2f;
    float x = 0, y = 0, z = 0;

    void Start()
    {
        gridAlign = GetComponent<EditorGrid>();
        gridAlign.enabled = false;
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        InvokeRepeating("Move", .5f, repeatRate);
        playerDir = Direction.none;
        lastDirLR = Direction.rightMov;
        lastDirUD = Direction.upMov;
    }

    void FixedUpdate()
    {
        count += Time.deltaTime;

        if (count > timeToGo)
        {
            if (playerDir == Direction.rightMov)
            {
                tr.position = new Vector3(tr.position.x + space * velocity, tr.position.y);
            }

            else if (playerDir == Direction.upMov)
            {
                tr.position = new Vector3(tr.position.x, tr.position.y + space * velocity);
            }

            else if (playerDir == Direction.downMov)
            {
                tr.position = new Vector3(tr.position.x, tr.position.y - space * velocity);
            }

            else if (playerDir == Direction.leftMov)
            {
                tr.position = new Vector3(tr.position.x - space * velocity, tr.position.y);
            }
            count = 0;
        }

        if (PlayerMovement.GetHasEnded())
            CancelInvoke();
    }
    
    void Move()
    {
        anim.Play("enemy_moving");
        if (leftRight)
        {
            if (lastDirLR == Direction.rightMov)
            {
                playerDir = Direction.leftMov;
                lastDirLR = playerDir;
            }

            else if (lastDirLR == Direction.leftMov)
            {
                playerDir = Direction.rightMov;
                lastDirLR = playerDir;
            }
        }

        if (upDown)
        {
            if (lastDirUD == Direction.upMov)
            {
                playerDir = Direction.downMov;
                lastDirUD = playerDir;
            }

            else if (lastDirUD == Direction.downMov)
            {
                playerDir = Direction.upMov;
                lastDirUD = playerDir;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Collides(collision);
        StartCoroutine(ToggleAlign());
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        Collides(collision);
    }

    void Collides(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            anim.Play("enemy_idle");
            playerDir = Direction.none;
        }
    }

    IEnumerator ToggleAlign()
    {
        yield return new WaitForSeconds(.05f);
        x = Mathf.Round(transform.position.x / cell_size) * cell_size;
        y = Mathf.Round(transform.position.y / cell_size) * cell_size;
        z = transform.position.z;
        transform.position = new Vector3(x, y, z);
    }
}
