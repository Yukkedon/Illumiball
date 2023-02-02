using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    Vector3 diff;

    public GameObject target;
    public float followSpeed;
    public Vector3 baseposition;

    // Start is called before the first frame update
    void Start()
    {
        baseposition= transform.position;
        diff = target.transform.position = transform.position;   
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.position = Vector3.Lerp(
            transform.position,
            target.transform.position + diff,
            Time.deltaTime * followSpeed);
        Vector3 posy = transform.position;
        posy.y = baseposition.y;
        transform.position= posy;

    }
}
