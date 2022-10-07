using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesBase : MonoBehaviour
{
    [Range(0.1f, 1000)]
    public float maxLife;

    [Space (5)]

    [Range (0, 10)]
    public float speed = 2;

    [Space(5)]

    [Range(1, 10)]
    public float speedRotation = 2;

    [Space(5)]

    [Range(0.1f, 100)]
    public float damage;

    [Space(5)]

    [Range(0.1f, 100)]
    public float resist;

    [Space(5)]

    [Header("Cooldowns config")]
    [Header("Element 0 Canceled / 1 front / 2 left / 3 right / 4 back / 5 neutral")]
    [Range(0, 10)]
    public float[] cooldowns = new float[5];

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
