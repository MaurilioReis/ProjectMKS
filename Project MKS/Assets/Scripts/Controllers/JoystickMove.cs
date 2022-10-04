using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    public RectTransform centerJoystick;
    public float reguleDistance = 150;

    Vector2 startPositionTouch;

    public CanvasGroup controlleOpacity;

    void Start()
    {
        if (centerJoystick == null)
        {
            // Pick child
        }
    }


    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(Input.touchCount-1);

            if(startPositionTouch == Vector2.zero)
            {
                controlleOpacity.alpha = 1;
                startPositionTouch = touch.position;
                transform.position = touch.position;
            }

            Vector2 distance = touch.position - startPositionTouch;

            centerJoystick.position = startPositionTouch + Vector2.ClampMagnitude(distance, reguleDistance);
        }
        else
        {
            startPositionTouch = Vector2.zero;

            if (controlleOpacity.alpha > 0)
                controlleOpacity.alpha -= Time.deltaTime;
        }
    }
}
