using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submerge : MonoBehaviour
{

    [Header("min and max time to start submerge")]
    public float minTimeToSubmerge;
    public float maxTimeToSubmerge;
    float timeSubmerge;

    [Header ("percentage chance of increase time to start submerge")]
    [Range(0, 100)]
    public int percentChanceIncreaseTime;
    public float timeIncrease;

    [Header("percentage chance of extending the time to submerge")]
    public SpriteRenderer[] spritesSubmerge;
    public Collider2D[] colliders;
    public GameObject[] fx;

    bool inSubmerge;

    void Start()
    {
        int dice = Random.Range(0, 100);
        if (dice < percentChanceIncreaseTime)
        {
            minTimeToSubmerge = minTimeToSubmerge + timeIncrease;
            maxTimeToSubmerge = maxTimeToSubmerge + timeIncrease;
        }

        timeSubmerge = Random.Range(minTimeToSubmerge, maxTimeToSubmerge);

        StartCoroutine("StartSubmerge");
    }

    IEnumerator StartSubmerge()
    {
        yield return new WaitForSecondsRealtime(timeSubmerge);

        foreach (SpriteRenderer sr in spritesSubmerge)
        {
            sr.sortingLayerName = "Submerse";
        }

        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

        foreach (GameObject go in fx)
        {
            go.SetActive(true);
        }

        inSubmerge = true;
    }


    private void FixedUpdate()
    {
        if (inSubmerge)
        {
            transform.localScale -= new Vector3(Time.deltaTime/10, Time.deltaTime / 10, Time.deltaTime / 10);

            if (transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}

