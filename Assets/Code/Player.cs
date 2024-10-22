using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody PC;
    public int speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = new Vector3(0,0,0);
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10;
        }
        else
        {
            speed = 5;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vel.x = speed;  
        }
        else if (Input.GetKey(KeyCode.A))
        {
            vel.x = -speed;  
        }
        if (Input.GetKey(KeyCode.W))
        {
            vel.z = speed;  
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vel.z = -speed;  
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            vel.y = speed+100;  
        }

        PC.velocity = vel;
    }
}
