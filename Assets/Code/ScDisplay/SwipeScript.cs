using UnityEngine;
using System.Collections;

public class SwipeScript : MonoBehaviour
{

    PlayerMovement pm;
    /// <summary>
    /// Distancia minima para que se relice swipe en eje Y
    /// </summary>
    public float minSwipeDistY = 130;//300
    /// <summary>
    /// Distancia minima para que se relice swipe en eje X
    /// </summary>
    public float minSwipeDistX = 130;//300
    /// <summary>
    /// Posición inicial del swipe
    /// </summary>
    private Vector2 startPos;
    /// <summary>
    /// touch
    /// </summary>
    Touch touch;
    bool touchActive = false;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
#if UNITY_ANDROID
        if (touchActive && pm.GetIfMovActive())
        {
            if (Input.touchCount > 0)
            {//Cuando se realice un touch
                touch = Input.touches[0];//Cuando solo haya un touch
                switch (touch.phase)
                {
                    case TouchPhase.Began://Inicio del touch
                        startPos = touch.position;
                        break;
                    case TouchPhase.Moved://Final del touch
                        float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

                        if (swipeDistVertical > minSwipeDistY)
                        {
                            float swipeValue = Mathf.Sign(touch.position.y - startPos.y);

                            if (swipeValue > 0)
                            {//up swipe
                                pm.SetPlayerDir(PlayerMovement.Direction.upMov);
                                pm.setMov(false);
                            }
                            else if (swipeValue < 0)
                            {//down swipe
                                pm.SetPlayerDir(PlayerMovement.Direction.downMov);
                                pm.setMov(false);
                            }
                        }

                        float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

                        if (swipeDistHorizontal > minSwipeDistX)
                        {
                            float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                            if (swipeValue > 0)
                            {//right swipe
                                pm.SetPlayerDir(PlayerMovement.Direction.rightMov);
                                pm.setMov(false);
                            }
                            else if (swipeValue < 0)
                            {//left swipe
                                pm.SetPlayerDir(PlayerMovement.Direction.leftMov);
                                pm.setMov(false);
                            }
                        }
                        break;
                }
            }

        }
#endif
    }

    public void OnLevelWasLoaded(int level)
    {
#if UNITY_ANDROID
        touchActive = false;
        StartCoroutine(EnableTouch());
#endif
    }

    IEnumerator EnableTouch()
    {
        yield return new WaitForSeconds(0.3f);
        touchActive = true;
    }
}