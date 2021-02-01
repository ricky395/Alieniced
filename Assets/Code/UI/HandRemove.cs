using UnityEngine;
using System.Collections;

public class HandRemove : MonoBehaviour
{
    public void OnMouseDown()
    {
        DelObject();
    }

    public void OnMouseDrag()
    {
        DelObject();
    }

    void DelObject()
    {
        GPlayclass.DisableFirstTime();
        Destroy(this.gameObject);
    }
}
