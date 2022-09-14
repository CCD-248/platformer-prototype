using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AttackDetails attackDetails;
    private float speed;
    private float travelDistance;
    private float xStartPosition;
    private Rigidbody2D rb;
    private bool isGravityOn;
    private bool hasHitGround;
    [SerializeField] private float damageRadius;
    [SerializeField] private float gravityScale;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform damagePosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = transform.right * speed;
        xStartPosition = transform.position.x;
        isGravityOn = false;
    }

    private void Update()
    {
        if (!hasHitGround)
        {
            attackDetails.position = transform.position;
            if (isGravityOn)
            {
                var angle = Mathf.Atan2(rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            var damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            var groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if (damageHit)
            {
                damageHit.transform.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }

            if (groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            }

            if (Mathf.Abs(xStartPosition - transform.position.x) >= travelDistance && isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = gravityScale;
            }
        }
    }

    public void FireProjectile(float speed, float travel, float damage)
    { 
        this.speed = speed;
        travelDistance = travel;
        attackDetails.amountOfDamage = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
