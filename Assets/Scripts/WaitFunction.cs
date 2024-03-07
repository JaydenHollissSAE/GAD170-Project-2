using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitFunction : MonoBehaviour
{
    public int secs;
    public int framesLeft;
    public bool waiting;
    // Start is called before the first frame update
    void Start()
    {
        secs = 10;
        wait();
    }

    public void wait()
    {
        waiting = true;
        while (framesLeft < secs*40000)
        {
            framesLeft += 1;
            //Debug.Log("passed");
        }
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!waiting)
        {
            Debug.Log("Time up!");
        }
    }
}
