using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float bulletmovespeed;
    public Rigidbody rb;
    public float bulletlife;
    public GameObject BulletEffect;
    public int damage = 1;
    public bool damageenemy,damageplayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * bulletmovespeed;
        bulletlife -= Time.deltaTime;
        if (bulletlife <= 0)
        {
            Destroy(gameObject);
        }
    }
   private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Enemy"&&damageenemy)
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemyHealth(damage);

        }

        if (other.gameObject.tag == "Player" && damageplayer)
        {
            Debug.Log("PlayerGotHit"+transform.position);
            //Destroy(other.gameObject);
            //other.gameObject.GetComponent<EnemyHealthController>().DamageEnemyHealth(damage);

        }
        Destroy(gameObject);
        Instantiate(BulletEffect, transform.position+(transform.forward*(-bulletmovespeed*Time.deltaTime)), transform.rotation);
       
    }
}
