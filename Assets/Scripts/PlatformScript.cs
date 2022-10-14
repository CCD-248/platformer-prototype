using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private int facingDirection = 1;

    public bool verticalPlatform = true;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float speed;


    protected Transform target;
    protected Vector2 workspace;
    protected delegate void MoveDelegate();
    protected MoveDelegate moveDelegate;


    private void Start()
    {
        target = transform;
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPos.position;
        if (verticalPlatform)
        {
            moveDelegate = MoveVertical;
        }
        else
        {
            moveDelegate = MoveHorizontal;
        }
    }



    private void FixedUpdate()
    {
        if (moveDelegate != null)
        {
            moveDelegate();
        }
    }

    protected virtual void MoveVertical()
    {
        if (transform.position.y > endPos.position.y)
        {
            facingDirection *= -1;
            target = startPos;
        }
        if (transform.position.y < startPos.position.y)
        {
            facingDirection *= -1;
            target = endPos; 
        }
        workspace.Set(0, speed * facingDirection);
        rb.velocity = workspace;
    }

    protected virtual void MoveHorizontal()
    {
        if (transform.position.x > endPos.position.x)
        {
            facingDirection *= -1;
            transform.position = endPos.position;
        }
        if (transform.position.x < startPos.position.x)
        {
            facingDirection *= -1;
            transform.position = startPos.position;
        }
        workspace.Set(speed * facingDirection, 0);
        rb.velocity = workspace;
    }
}
