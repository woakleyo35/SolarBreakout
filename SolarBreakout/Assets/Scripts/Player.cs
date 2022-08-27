using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigidBody;

    private void Start()
    {
        this.rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var mouseX = Vector3.right * Input.mousePosition.x;
        var screenX = Camera.main.ScreenToWorldPoint(mouseX).x; 

        const float paddleY = -17;
        var newPosition = new Vector3(screenX, paddleY, 0);
        this.rigidBody.MovePosition(newPosition);
    }
}
