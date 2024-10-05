using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.position - target.position;

        if(direction.sqrMagnitude > 25f)
        { 
            transform.Translate(direction.normalized * Time.deltaTime, Space.World);
            transform.forward = direction.normalized;

        }
    }
}
