using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    public float speedX = 10f;
    public float speedY = 2f;
    public float maxX = 8.8f;
    public bool goingRight = true;

    void Awake()
    {
        goingRight = Random.Range(0, 2) == 1 ? true : false;
    }

    void Update()
    {
        transform.Translate(speedX * Time.deltaTime * (goingRight ? Vector3.right : Vector3.left));
        transform.Translate(speedY * Time.deltaTime * Vector3.down);
        if (transform.position.x >= maxX) goingRight = false;
        if (transform.position.x <= -maxX) goingRight = true;
    }
}
