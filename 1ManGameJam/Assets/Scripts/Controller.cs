using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class Controller : MonoBehaviour
{
    public Boundary boundary;
    public float speedX, speedY;
    public float tilt;

    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rigidbody2D.velocity = new Vector3(moveHorizontal * speedX, moveVertical * speedY);

        rigidbody2D.position = new Vector3
            (
            Mathf.Clamp(rigidbody2D.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rigidbody2D.position.y, boundary.yMin, boundary.yMax)
            );

        transform.rotation = Quaternion.Euler(0.0f, rigidbody2D.velocity.x * (-tilt), 0.0f);
    }
}
