using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float enemymovespeed;
    public Rigidbody rb;
    bool chasing;
    public float distancetochase = 10f,distancetoloose=15f;
    Vector3 targetpoint;
    public GameObject enemybullet;
    public Transform enemyfirepoint;
    public float firerate,waitbetweenshots=2f,timetoshoot=1f;
    public float firecount,shotscounter,shoottimecounter;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shoottimecounter = timetoshoot;
        shotscounter = waitbetweenshots;
    }

    // Update is called once per frame
    void Update()
    {
        //targetpoint = PlayerController.instance.transform.position;
        //targetpoint.y = transform.position.y;
        if(!chasing)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < distancetochase);
            {
                if (PlayerController.instance.isgrounded == false)
                {
                    chasing = false;
                    //firecount = 2f;
                    shoottimecounter = timetoshoot;
                    shotscounter = waitbetweenshots;


                }
                else
                {
                    chasing = true;
                }
            }
        }
        else
        {
           if(PlayerController.instance.isgrounded==true)
            {
                transform.LookAt(PlayerController.instance.transform.position);
                rb.velocity = transform.forward * enemymovespeed;

            }
            if (Vector3.Distance(transform.position,PlayerController.instance.transform.position)>distancetoloose)
            {
                chasing = false;
            }

            if (shotscounter > 0)
            {
                shotscounter -= Time.deltaTime;
                if(shotscounter<=0)
                {
                    shoottimecounter = timetoshoot;
                }
            }
            else
            {
                shoottimecounter -= Time.deltaTime;

                if (shoottimecounter > 0)
                {
                    firecount -= Time.deltaTime;
                    if (firecount <= 0)
                    {
                        firecount = firerate;
                        Instantiate(enemybullet, enemyfirepoint.position, enemyfirepoint.rotation);
                    }
                }
                else
                {
                    shotscounter = waitbetweenshots;
                }
                
            }
            
        }

    }
}
