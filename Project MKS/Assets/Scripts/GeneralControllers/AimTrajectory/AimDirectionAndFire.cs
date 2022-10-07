using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDirectionAndFire : MonoBehaviour
{
    [HideInInspector] public bool inAtack = false;

    [HideInInspector] public bool triggerFire;
    [HideInInspector] public int sideAim;
    [HideInInspector] public int lockAim;

    [Header("attributes based on:")]
    public AttributesBase basedAttributes;

    [Space(10)]
    [Header("Animations trajectory")]
    public Animator trajectorysAnim;


    [Space(10)]
    [Header("Spawns config")]
    [Header("Element 0 Canceled / 1 front / 2 left / 3 right / 4 back / 5 neutral")]

    [HideInInspector] public float[] cooldowns;

    public ArmamentParameters[] weapons;

    [Space(10)]
    Transform[] spawnOrigins;
    public float timeToSpawn = 0.1f;
    public float timeBetweenSpawns = 0.1f;


    private void Start()
    {
        cooldowns = basedAttributes.cooldowns;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            
        if (collision.name == "TriggerFront")    
        {
            sideAim = 1;
        }
  
        if (collision.name == "TriggerLeft")  
        { 
            sideAim = 2;
        }

        if (collision.name == "TriggerRight") 
        {      
            sideAim = 3;
        }
 
        if (collision.name == "TriggerBack")  
        {     
            sideAim = 4;
        }
    }

    private void Update()
    {
        if (trajectorysAnim != null)
        {
            if (triggerFire == false)
            {
                trajectorysAnim.SetInteger("SideAim", lockAim);
            }
            else
            {
                trajectorysAnim.SetInteger("SideAim", sideAim);
            }
        }
    }

    public IEnumerator Fire()
    {
        inAtack = true;

        int registerSide = sideAim;
        spawnOrigins = weapons[registerSide].originsSpawn;
        weapons[registerSide].inAtack = true;

        yield return new WaitForSecondsRealtime(timeToSpawn);

        for (int nSpawn = 0; nSpawn < spawnOrigins.Length; nSpawn++)
        {

            if (spawnOrigins[nSpawn] != null && spawnOrigins[nSpawn].gameObject.activeSelf)
            {
                GameObject instance = Instantiate(weapons[1].ammunition, spawnOrigins[nSpawn].position, spawnOrigins[nSpawn].rotation) as GameObject;
                Rigidbody2D rbSpawns = instance.GetComponent<Rigidbody2D>();
                rbSpawns.AddForce(spawnOrigins[nSpawn].transform.right * 150, ForceMode2D.Force);

                // audio

                yield return new WaitForSecondsRealtime(timeBetweenSpawns);
            }
        }
        weapons[registerSide].inAtack = false;
        inAtack = false;
    }
}
