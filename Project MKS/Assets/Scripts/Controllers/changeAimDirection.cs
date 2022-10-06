using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeAimDirection : MonoBehaviour
{
    [Header("SideAim")]
    [Header ("0 back / 1 front / 2 left / 3 right")]
    [Range(0,3)]
    public int sideAim;

    [Space(10)]
    //[Header("animations")]
    public Animator trajectorysAnim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "VerifyAim")
        {
            trajectorysAnim.SetInteger("SideAim", sideAim);

            Debug.Log("Entrou");
        }
    }
}
