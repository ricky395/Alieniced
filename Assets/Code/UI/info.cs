using UnityEngine;
using System.Collections;

public class info : MonoBehaviour
{
    public Transform panelPref;
    public Transform EventSys;
    Transform panelObj;
    Transform EventObj;
    GameObject[] collObjects;
    BoxCollider2D[] allBoxColl = new BoxCollider2D[4];
    PlayerMovement playerMov;

    void Start()
    {
        playerMov = GameObject.Find("Player").GetComponent<PlayerMovement>();
        InitBoxColliders();
    }

    public void OnMouseUp()
    {
        ToggleColliders(false);
        panelObj = Instantiate(panelPref);
        EventObj = Instantiate(EventSys);
    }

    public void OnLevelWasLoaded(int level)
    {
        InitBoxColliders();
    }

    void InitBoxColliders()
    {
        collObjects = GameObject.FindGameObjectsWithTag("MenuColliders");
        for (int i = 0; i < collObjects.Length; i++)
        {
            allBoxColl[i] = collObjects[i].GetComponent<BoxCollider2D>();
        }
    }

    public void ToggleColliders(bool state)
    {
        for (int i = 0; i < allBoxColl.Length; i++)
        {
            allBoxColl[i].enabled = state;
        }

        playerMov.SetInfo(state); //activa y desactiva el input de movimiento del alien

        SwipeScript swipe = playerMov.GetComponent<SwipeScript>();
        if (state)
        {
            Time.timeScale = 1;
            swipe.enabled = true;
        }
        else
        {
            swipe.enabled = false;
            Time.timeScale = 0;
        }
    }

    public BoxCollider2D[] GetAllColliders()
    {
        return allBoxColl;
    }
}
