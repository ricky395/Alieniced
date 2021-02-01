using UnityEngine;
using System.Collections;

public class ButtonTrigger : MonoBehaviour
{
    Transform playertr;
    ButtonController bc;
    Animator animator;
    bool hasEntered;

    void Start()
    {
        playertr = GameObject.Find("Player").GetComponent<Transform>();
        bc = GetComponentInParent<Transform>().GetComponentInParent<ButtonController>();
        hasEntered = false;
    }

    public void SetAnimator(Animator anim)
    {
        animator = anim;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasEntered && collision.transform == playertr)
        {
            animator.SetTrigger("doorState");
            StartCoroutine(bc.ButtonState());
        }
        hasEntered = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        hasEntered = false;
    }

    public void OnTriggerStay2D()
    {

    }
}
