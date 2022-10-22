using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followBall : MonoBehaviour
{
    public Transform ob;

    // Update is called once per frame
    void Update()
    {
        float x;

        if (ob.position.x < 0 || ob.position.x > 40)
        {
            x = transform.position.x;
        } else
        {
            x = ob.position.x;
        }
        
        transform.position = new Vector3(x, ob.position.y+1, transform.position.z);
    }
}
