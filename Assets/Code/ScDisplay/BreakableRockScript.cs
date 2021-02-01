using UnityEngine;
using System.Collections;

public class BreakableRockScript : MonoBehaviour
{
    /// <summary>
    /// Array de los hijos del elemento base
    /// </summary>
    Transform[] objArr;
    /// <summary>
    /// Siguiente hijo a destruir
    /// </summary>
    int nextToBreak;
    /// <summary>
    /// Componente de audio
    /// </summary>
    AudioController ac;

    /// <summary>
    /// Obtiene el componente audiocontroller del gameobject scenemanager y se meten en un array los hijos del elemento base
    /// </summary>
    void Start()
    {
        nextToBreak = 1;
        ac = GameObject.Find("SceneManager(Clone)").GetComponent<AudioController>();
        objArr = GetComponentsInChildren<Transform>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /// <summary>
        /// Al colisionar se elimina el hijo correspondiente al elemento base y se reproduce un sonido hasta llegar al ultimo donde finalmente desaparece y se reproduce un sonido distinto
        /// </summary>
        if (collision.gameObject.tag == "Player" && GameObject.Find("Player").GetComponent<PlayerMovement>().timeMoving > 0.1)
        {
            Destroy(objArr[nextToBreak].gameObject);
            if (nextToBreak + 1 < objArr.Length)
            {
                nextToBreak++;
                ac.PlayRockBreak();
            }

            else
            {
                objArr[0].GetComponent<BoxCollider2D>().enabled = false;
                ac.PlayRockFinalBreak();
            }
        }
    }
}
