using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    Transform[] buttonsAndDoor; //los 5 objetos del prefab
    Transform[] lineObjects; //3 objetos
    Transform greenButton;
    SpriteRenderer buttonRend;
    Animator animator;
    BoxCollider2D doorCollider;
    AudioController ac;
    ShowLines lines;

    void Start()
    {        
        InitLineObjtects();
        greenButton = buttonsAndDoor[1].GetChild(0);
        InitLines();

        buttonRend = greenButton.GetComponent<SpriteRenderer>();
        animator = buttonsAndDoor[4].GetComponent<Animator>();
        greenButton.GetComponent<ButtonTrigger>().SetAnimator(animator);
        doorCollider = buttonsAndDoor[4].GetComponent<BoxCollider2D>();
        ac = GameObject.Find("SceneManager(Clone)").GetComponent<AudioController>();
    }
    
    public IEnumerator ButtonState()
    {
        yield return new WaitForSeconds(0.2f);
        
        if (buttonRend.enabled)
        {
            buttonRend.enabled = false;
            doorCollider.enabled = false;
            ac.PlayGreenButton();
        }
        else
        {
            buttonRend.enabled = true;
            doorCollider.enabled = true;
            ac.PlayRedButton();
        }
    }

    void InitLineObjtects()
    {
        lineObjects = new Transform[3];
        buttonsAndDoor = GetComponentsInChildren<Transform>();
        for (int i = 0; i < 3; i++)
        {
            lineObjects[i] = buttonsAndDoor[i + 2];
        }
    }

    void InitLines()
    {
        lines = GetComponent<ShowLines>();
    }

    public void OnDrawGizmos()
    {
        if (lineObjects == null)
            InitLineObjtects();
        if (lines == null)
            InitLines();

        lines.Show(lineObjects);
    }
}
