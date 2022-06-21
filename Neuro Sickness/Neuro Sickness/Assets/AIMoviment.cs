using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoviment : MonoBehaviour
{
    public GameObject position;
    public float speed;

    void Update()
    {
        float d = Vector2.Distance(transform.position, position.transform.position);
        transform.position = Vector2.MoveTowards(transform.position, position.transform.position, speed * d);    
    }
}
