using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickFire : MonoBehaviour
{
    Touch touchJoystic;
    Vector2 startPositionTouch;
    RectTransform centerJoystick;

    Animator arcAnim;

    CanvasGroup alphaController;

    [Header("Configurations Joystic")]
    public float reguleDistance = 200;
    [Space(10)]
    public Image icon;
    public Sprite[] iconsSprites;
    [Space(10)]
    public Image iconBackGround;
    public Sprite[] iconsSpritesBg;

    [Space (10)]
    [Header("Configurations path aim")]
    public Transform rotTrajectoryController;
    public TrajectoryDirection scriptTrajectory;

    [Space(10)]
    [Header("Spawns to fire")]
    public GameObject[] Spawns;
    public int amountSpawns;
    Rigidbody2D rbSpawns;


    void Start()
    {
        touchJoystic = new Touch { fingerId = -1 };

        centerJoystick = gameObject.transform.GetChild(2).GetComponent<RectTransform>();
        arcAnim = gameObject.transform.GetChild(1).GetComponent<Animator>();

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

                    icon.sprite = iconsSprites[0];
                    iconBackGround.sprite = iconsSpritesBg[0];

                    // if cooldown
                    // FIRE
                    if(scriptTrajectory.triggerFire == true)
                    {
                        Vector2 posSpawn = new Vector2(rotTrajectoryController.position.x + 0.6f, rotTrajectoryController.position.y);
                        GameObject instance = Instantiate(Spawns[0], posSpawn, rotTrajectoryController.rotation) as GameObject;
                        rbSpawns = instance.GetComponent<Rigidbody2D>();
                        rbSpawns.AddForce(rotTrajectoryController.right * 150, ForceMode2D.Force);
                        scriptTrajectory.triggerFire = false;
                    }

                    scriptTrajectory.lockAim = 0;

                    arcAnim.SetInteger("InsideOutside", 0);
                }
                else // Movement
                {
                    Vector2 direction = touchJoystic.position - startPositionTouch;
                    centerJoystick.position = startPositionTouch + Vector2.ClampMagnitude(direction, reguleDistance);

                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    Vector2 distanceMoveJoystick = (Vector2)centerJoystick.position / reguleDistance - (Vector2)gameObject.transform.position / reguleDistance;

                    rotTrajectoryController.transform.rotation = Quaternion.Slerp(rotTrajectoryController.transform.rotation, Quaternion.Euler(0, 0, angle), 100 * Time.deltaTime);

                    icon.sprite = iconsSprites[scriptTrajectory.sideAim];

                    if (distanceMoveJoystick.magnitude < 0.75f)
                    {
                        iconBackGround.sprite = iconsSpritesBg[0];

                        scriptTrajectory.triggerFire = false;
                        scriptTrajectory.lockAim = 5;

                        arcAnim.SetInteger("InsideOutside", 1);
                    }
                    else
                    {
                        iconBackGround.sprite = iconsSpritesBg[scriptTrajectory.sideAim];

                        scriptTrajectory.triggerFire = true;

                        arcAnim.SetInteger("InsideOutside", 2);
                    }
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