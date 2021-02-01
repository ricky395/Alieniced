using UnityEngine;
using System.Collections;

public class StuckAchievement : MonoBehaviour
{

    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            yield return new WaitForSeconds(3);
            GPlayclass.UnlockAchievement("CgkI-Meyi84DEAIQAw"); //stuck achievement
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {

    }
}
