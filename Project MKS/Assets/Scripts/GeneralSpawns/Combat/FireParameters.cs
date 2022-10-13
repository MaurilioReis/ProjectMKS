using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParameters : MonoBehaviour
{
    [Header("Damage")]
    public float dmg;

    [Space(10)]

    [Header("Spawns in start")]
    public GameObject[] spawnsStart;
    [Space(5)]
    [Header("Spawns in timer")]
    public GameObject[] spawnsInTimer;
    public float timerSpawns;
    [Space(5)]
    [Header("Spawns impact")]
    public GameObject[] spawnsImpactWoods;
    [Space(5)]
    [Header("Spawn at end of max distance")]
    public GameObject[] spawnsEndDistance;
    [Space(5)]
    [Header("Ignore collision on distance")]
    public Collider2D[] colliderDisabled;
    public float distanceActiveColliders;

    Vector2 startPosition;
    float distance;

    [HideInInspector] public float maxDistance;

    void Start()
    {
        startPosition = gameObject.transform.position;

        foreach (GameObject spawn in spawnsStart)
        {
            GameObject intance = Instantiate(spawn, gameObject.transform.position, gameObject.transform.rotation);
        }

        if (timerSpawns > 0)
        {
            StartCoroutine("TimerSpawns");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            AttributesBase scriptAttributes = collision.gameObject.GetComponent<AttributesBase>();
            scriptAttributes.applyDmg(dmg);

            foreach (GameObject spawn in spawnsImpactWoods)
            {
                GameObject intance = Instantiate(spawn, gameObject.transform.position, gameObject.transform.rotation);
            }

            Destroy(gameObject);
        }
    }

    IEnumerator TimerSpawns()
    {
        yield return new WaitForSecondsRealtime(timerSpawns);

        foreach (GameObject spawn in spawnsInTimer)
        {
            GameObject intance = Instantiate(spawn, gameObject.transform.position, gameObject.transform.rotation);
            // Submerse
        }
    }

    private void Update()
    {
        distance = Vector2.Distance(startPosition, transform.position);

        if (maxDistance > 0 && distance >= maxDistance)
        {
            foreach (GameObject spawn in spawnsEndDistance)
            {
                GameObject intance = Instantiate(spawn, gameObject.transform.position, gameObject.transform.rotation);
                // Submerse
            }

            Destroy(gameObject);
        }

        if (distanceActiveColliders > 0 && distance >= distanceActiveColliders)
        {
            foreach (Collider2D col in colliderDisabled)
            {
                col.enabled = true;
            }

            maxDistance = 0;
        }     
    }

}
