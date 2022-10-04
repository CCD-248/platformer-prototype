using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPos.position;
    }

    public Vector2 vector;

    private void Update()
    {
        vector = new Vector2 (0, speed);
        rb.velocity = vector;
    }
}
