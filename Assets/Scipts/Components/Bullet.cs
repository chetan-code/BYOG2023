using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float destroyAfter = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyAfter); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.forward * speed * Time.deltaTime;  
    }
}
