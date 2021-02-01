using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour
{
    public void OnMouseUp()
    {
        DeathScript.ResetDeaths();
        PlayerMovement.NextLevel();
    }
}
