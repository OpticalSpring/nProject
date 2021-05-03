using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXItem : MonoBehaviour
{
    public float Duration;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(Duration > 0)
        {
            Duration -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
