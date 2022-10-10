using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float dmg;
    public float timeToDestroyer;

    public GameObject prefabSmokeFire;
    public GameObject prefabMiniSparks;
    public GameObject prefabExplosion;
    public GameObject prefabImpact;
    public GameObject prefabImpactWhater;

    void Start()
    {
        GameObject smokeFire = Instantiate(prefabSmokeFire, gameObject.transform.position, gameObject.transform.rotation);
        GameObject miniSparks = Instantiate(prefabMiniSparks, gameObject.transform.position, gameObject.transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AttributesBase scriptAttributes = collision.gameObject.GetComponent<AttributesBase>();
            scriptAttributes.applyDmg(dmg);
        }

        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
