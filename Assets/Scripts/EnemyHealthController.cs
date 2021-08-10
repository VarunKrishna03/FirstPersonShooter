using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int currenthealth = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DamageEnemyHealth(int damage)
    {
        currenthealth -= 1;
        if(currenthealth<=0)
        {
            Destroy(gameObject);
        }
    }
}
