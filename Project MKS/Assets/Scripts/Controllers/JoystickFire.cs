using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickFire : MonoBehaviour
{
    Touch touchJoystic;
    Vector2 startPositionTouch;
    RectTransform centerJoystick;
    CanvasGroup alphaController;

    [Header("Configurations Joystic")]
    public float reguleDistance = 150;

    public Transform frontalFirePath;
    public Transform sideFirePath;



    void Start()
    {
        touchJoystic = new Touch { fingerId = -1 };

        centerJoystick = gameObject.transform.GetChild(1).GetComponent<RectTransform>();

        startPositionTouch = centerJoystick.position;

        alphaController = gameObject.GetComponent<CanvasGroup>();
    }


    void Update()
    {
        if(Input.touchCount > 0)
        {
            for(int numberTouch = 0; numberTouch < Input.touchCount; numberTouch++ )
            {

                if(touchJoystic.fingerId == -1)
                {
                    // Register touch
                    if (Input.GetTouch(numberTouch).position.x > Screen.width / 2 && Input.GetTouch(numberTouch).position.y < Screen.height / 2)
                    {
                        touchJoystic = Input.GetTouch(numberTouch);

                        alphaController.alpha = 1;
                        startPositionTouch = touchJoystic.position;
                        transform.position = touchJoystic.position;
                    }
                }
                else 
                { 
                    if (Input.GetTouch(numberTouch).fingerId == touchJoystic.fingerId)
                    {
                        touchJoystic = Input.GetTouch(numberTouch);
                    }
                }
            }

            if (touchJoystic.fingerId != -1)
            {
                // Drop
                if (touchJoystic.phase == TouchPhase.Canceled || touchJoystic.phase == TouchPhase.Ended)
                {
                    touchJoystic = new Touch { fingerId = -1 };
                }
                else // Movement
                {
                    Vector2 direction = touchJoystic.position - startPositionTouch;
                    centerJoystick.position = startPositionTouch + Vector2.ClampMagnitude(direction, reguleDistance);

                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    Vector2 distanceMoveJoystick = (Vector2)centerJoystick.position - (Vector2)gameObject.transform.position;
                    Vector2 directionJoystick = new Vector2 (distanceMoveJoystick.x / reguleDistance, distanceMoveJoystick.y / reguleDistance);

                    frontalFirePath.transform.rotation = Quaternion.Slerp(frontalFirePath.transform.rotation, Quaternion.Euler(0, 0, angle), 100 * Time.deltaTime);
                }
            }
        }

        //unselected
        if (alphaController.alpha > 0 && touchJoystic.fingerId == -1)
        {
            centerJoystick.position = Vector2.MoveTowards(centerJoystick.position, startPositionTouch, 10);
            alphaController.alpha -= Time.deltaTime;
        }
    }
}