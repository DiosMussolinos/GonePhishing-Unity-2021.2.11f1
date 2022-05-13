//Gabriel 'DiosMussolinos' Vergari
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAnimation : MonoBehaviour
{
    public float constAdd;
    public float angle = 0;

    // Update is called once per frame
    void Update()
    {
        if(angle >= 360)
        {
            angle = 0;
        }

        angle += constAdd;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);

    }
}
