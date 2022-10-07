using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [Range(0.1f, 1000)]
    public float maxLife;

    [Range (0, 10)]
    public float speed = 2;

    [Range(1, 10)]
    public float speedRotation = 2;

    [Range(0.1f, 100)]
    public float damage;

    [Range(0.1f, 100)]
    public float resist;

    [Range(0, 10)]
    public float cooldownFront;

    [Range(0, 10)]
    public float cooldownSideR;

    [Range(0, 10)]
    public float cooldownSideL;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
