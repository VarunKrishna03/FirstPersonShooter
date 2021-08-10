using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    public Transform enemyrotate;
    public float movespeed,rotatespeed;
    public bool shouldmove, shouldrotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldmove)
        {
            transform.position += new Vector3(movespeed, 0, 0) * Time.deltaTime;
        }
        if(shouldrotate)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, rotatespeed * Time.deltaTime, 0));
        }
    }
}
