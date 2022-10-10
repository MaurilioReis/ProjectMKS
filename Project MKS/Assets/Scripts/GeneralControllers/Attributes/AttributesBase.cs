using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class AttributesBase : MonoBehaviour
{
    [Range(0.1f, 1000)]
    public float maxLife;

    [Space(5)]

    [Range(0, 10)]
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

    [Space(10)]
    [Header("Life Bar")]
    public GameObject prefabBar;
    PositionConstraint constraint;
    ConstraintSource myConstraintSource;
    Image fillBar;
    float valueLifeBar;

    void Start()
    {
        GameObject instantiateLifeBar = Instantiate(prefabBar) as GameObject;

        constraint = instantiateLifeBar.GetComponent<PositionConstraint>();
        myConstraintSource.sourceTransform = transform;
        myConstraintSource.weight = 1f;
        constraint.AddSource(myConstraintSource);

        fillBar = instantiateLifeBar.transform.GetChild(1).GetComponent<Image>();
        valueLifeBar = maxLife;
    }

    public void applyDmg(float valueDmg)
    {
        valueLifeBar -= valueDmg;
        //Debug.Log("Value fill" + valueLifeBar / maxLife);
        fillBar.fillAmount = valueLifeBar / maxLife;

        Debug.Log("life: " + valueLifeBar + " / Receive: " + valueDmg + " / Current life: " + fillBar.fillAmount * 500);
    }
}
