using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInstance : MonoBehaviour
{

    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(
                ball,
                new Vector3(Random.Range(-5f, 5f), Random.Range(5f, 15f), Random.Range(-5f, 5f)),
                Quaternion.identity);
        }

    }
}
