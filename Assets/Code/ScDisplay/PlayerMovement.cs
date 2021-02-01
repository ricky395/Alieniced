using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Dirección del jugador
    /// </summary>
    public enum Direction { none, upMov, rightMov, downMov, leftMov };

    /// <summary>
    /// Objeto que define la dirección del jugador
    /// </summary>
    Direction playerDir;

    /// <summary>
    /// Transform del jugador
    /// </summary>
    Transform tr;

    /// <summary>
    /// Define si el personaje está moviéndose o no
    /// </summary>
    bool isMovActive = true;

    /// <summary>
    /// Distancia mínima que recorre el personaje en cada movimiento por frame
    /// </summary>
    public float space = 0.2f;

    /// <summary>
    /// Cuenta que junto con timeToGo establecen la velocidad de movimiento
    /// </summary>
    float count = 0;

    /// <summary>
    /// Cada cuanto tiempo hace el jugador un movimiento (velocidad de cambio de movimiento)
    /// </summary>
    public float timeToGo = 0.03f;

    /// <summary>
    /// Transform del objeto meta en cada escena
    /// </summary>
    Transform goalTr;

    /// <summary>
    /// Componente de audio para los sonidos del personaje
    /// </summary>
    AudioController ac;

    /// <summary>
    /// Componente animator para establecer sus animaciones
    /// </summary>
    Animator anim;

    float x, y, z, cellSize;
    Vector3 direction;
    public float timeMoving;
    GameObject sceneObj;
    static lvlManager lvlManag;
    GameObject scm;
    bool infoNotActive = true;
    public float distance = 0.8f;
    static int sceneNo;
    TimeCounter timeC;
    static bool hasEnded = false;

    /// <summary>
    /// El Start se encarga de inicializar todos los componentes de la escena que incumben al personaje
    /// </summary>
    void Start()
    {
        scm = GameObject.Find("SceneManager(Clone)");
        lvlManag = scm.GetComponent<lvlManager>();
        cellSize = GetComponent<EditorGrid>().cell_size;
        anim = GetComponent<Animator>();
        GetComponent<EditorGrid>().enabled = false;
        tr = GetComponent<Transform>();
        playerDir = Direction.none;
        timeC = GetComponent<TimeCounter>();

        goalTr = GameObject.Find("Goal").GetComponent<Transform>();
        ac = scm.GetComponent<AudioController>();
    }

    public void SetInfo(bool state)
    {
        infoNotActive = state;
    }

    /// <summary>
    /// El Update se encarga del movimiento del personaje y de comprobar si ha llegado a la meta
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameEnded();
        }

        if (isMovActive && infoNotActive)
        {
            timeMoving = 0;
            PosUpdate();
            AnimationPlay(false);

            if (Input.GetKeyDown(KeyCode.D))
            {
                playerDir = Direction.rightMov;
                setMov(false);
            }

            else if (Input.GetKeyDown(KeyCode.W))
            {
                playerDir = Direction.upMov;
                setMov(false);
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                playerDir = Direction.downMov;
                setMov(false);
            }

            else if (Input.GetKeyDown(KeyCode.A))
            {
                playerDir = Direction.leftMov;
                setMov(false);
            }
        }

        else
        {
            AnimationPlay(true);
            GetObstacle();
            count += Time.deltaTime;
            timeMoving += Time.deltaTime;

            if (count > timeToGo)
            {
                if (playerDir == Direction.rightMov)
                {
                    //rb.velocity = new Vector2(speed, 0);
                    tr.position = new Vector3(tr.position.x + space, tr.position.y);
                }

                else if (playerDir == Direction.upMov)
                {
                    //rb.velocity = new Vector2(0, speed);
                    tr.position = new Vector3(tr.position.x, tr.position.y + space);
                }

                else if (playerDir == Direction.downMov)
                {
                    //rb.velocity = new Vector2(0, -speed);
                    tr.position = new Vector3(tr.position.x, tr.position.y - space);
                }

                else if (playerDir == Direction.leftMov)
                {
                    //rb.velocity = new Vector2(-speed, 0);
                    tr.position = new Vector3(tr.position.x - space, tr.position.y);
                    GPlayclass.UpdateEvent("CgkI-Meyi84DEAIQCg", 1);
                }
                count = 0;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {

    }

    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Goal")
        {
            yield return new WaitForSeconds(.18f);
            GameEnded();
        }
    }

    void GameEnded()
    {
        ac.PlayGoal();
        SceneController.LoadEndingBox();
        SceneEnded();
        AnimationPlay(false);
        GetComponent<DeathScript>().enabled = false;
        TimeCounter.ToggleTimerActive(false);
        this.enabled = false;
        EndGameVariables.UpdateCurrentNumbers(timeC.timeInSec - 1, DeathScript.GetDeaths());
    }

    public void SetPlayerDir(Direction dir)
    {
        playerDir = dir;
    }

    void PosUpdate()
    {
        x = Mathf.Round(transform.position.x / cellSize) * cellSize;
        y = Mathf.Round(transform.position.y / cellSize) * cellSize;
        z = transform.position.z;
        transform.position = new Vector3(x, y, z);
    }

    /// <summary>
    /// Carga las animaciones del personaje
    /// </summary>
    /// <param name="animate">Define si el jugador tiene animación de idle o de movimiento</param>
    void AnimationPlay(bool animate)
    {
        if (animate)
            anim.Play("player_anim");

        else anim.Play("player_idle");

    }

    void SetDirection()
    {
        direction = Vector3.zero;

        if (playerDir == Direction.rightMov)
            direction = transform.right;
        else if (playerDir == Direction.upMov)
            direction = transform.up;
        else if (playerDir == Direction.leftMov)
            direction = -transform.right;
        else if (playerDir == Direction.downMov)
            direction = -transform.up;
    }

    /// <summary>
    /// Se encarga de controlar las colisiones del personaje
    /// </summary>
    void GetObstacle()
    {
        SetDirection();

        Debug.DrawRay(transform.position, direction);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, LayerMask.GetMask("Obstacles"));

        if (hit.collider != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            SetDirectionNone();
            isMovActive = true;
        }
    }

    public void SetDirectionNone()
    {
        playerDir = Direction.none;
    }

    /// <summary>
    /// Setter del booleano de movimiento del personaje
    /// </summary>
    /// <param name="state"></param>
    public void setMov(bool state)
    {
        isMovActive = state;
    }

    /// <summary>
    /// Getter del booleano de movimiento del personaje
    /// </summary>
    /// <returns></returns>
    public bool GetIfMovActive()
    {
        return isMovActive;
    }

    /// <summary>
    /// Setter de la posición del personaje 
    /// </summary>
    /// <param name="pos"></param>
    public void setPos(Vector2 pos)
    {
        this.transform.position = pos;
    }

    void SceneEnded()
    {
        hasEnded = true;
        sceneNo = SceneController.GetSceneNo();

        if (!NotLastPackLevel(sceneNo))
            lvlManag.UnlockNextPack(sceneNo);
        MenuLevelsManager.UpdateLevel();
    }

    static bool NotLastPackLevel(int sceneNo)
    {
        return (sceneNo != 20 && sceneNo != 40 && sceneNo != 60);
    }

    /// <summary>
    /// Controla el nivel siguiente a cargar. Si es el último, vuelve al menú
    /// </summary>
    public static void NextLevel()
    {
        sceneNo = SceneController.GetSceneNo();

        if (NotLastPackLevel(sceneNo))
            SceneManager.LoadScene(sceneNo + 1);
        else
            SceneManager.LoadScene("packSelector");
    }

    public static bool GetHasEnded()
    {
        return hasEnded;
    }

    public void OnLevelWasLoaded(int level)
    {
        StopPlayer();
        hasEnded = false;
    }

    IEnumerator StopPlayer()
    {
        yield return new WaitForSeconds(.1f);
        playerDir = Direction.none;
    }
}