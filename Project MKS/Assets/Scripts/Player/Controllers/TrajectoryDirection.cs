using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryDirection : MonoBehaviour
{
    [Header("SideAim")]
    [Header ("0 Canceled / 1 front / 2 left / 3 right / 4 back / 5 neutral")]
    [Range(0,5)]
    [SerializeField] public bool triggerFire;
    [SerializeField] public int sideAim;
    [SerializeField] public int lockAim;

    [Space(10)]
    [Header("Animations trajectory")]
    public Animator trajectorysAnim;

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
        if(triggerFire == false)
        {
            trajectorysAnim.SetInteger("SideAim", lockAim);
        }
        else
        {
            trajectorysAnim.SetInteger("SideAim", sideAim);
        }
    }
}
