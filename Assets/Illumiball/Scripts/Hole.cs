using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    const float downSpeed = 0.9f;
    const float growSpeed = -50.0f;
    const float pushSpeed = 80.0f;

    bool isHolding;

    public string targetTag;

    public bool IsHolding()
    {
        return isHolding;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == targetTag)
        {
            isHolding= true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == targetTag)
        {
            isHolding= false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Rigidbody r = other.gameObject.GetComponent<Rigidbody>();

        Vector3 direction = other.gameObject.transform.position - transform.position;
        direction.Normalize();

        if (other.gameObject.tag == targetTag)
        {
            r.velocity *= downSpeed;
            r.AddForce(direction * growSpeed,ForceMode.Acceleration);
        }
        else
        {
            r.AddForce(direction * pushSpeed,ForceMode.Acceleration);
        }
    }
}
