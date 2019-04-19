using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1;
    public float crouchSpeedMultiplier = .5f;
    //走出平台后仍能跳跃的时间
    public float coyoteTime = .05f;

    public float jumpForce = 6f;
    public float crouchJumpForceMultiplier = 1.5f;

    public bool isOnGround;
    public bool isJumping;
    public bool isCrouching;

    PlayerInput playerInput;
    Rigidbody2D rb;

    public float crouchColliderMultiplier = .5f;
    BoxCollider2D bodyCollider;

    Vector2 bodyColliderSize;
    Vector2 bodyColliderOffset;
    Vector2 crouchColliderSize;
    Vector2 crouchColliderOffset;

    public LayerMask groundLayer;

    const float skinWidth = .1f;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();

        bodyColliderSize = bodyCollider.size;
        bodyColliderOffset = bodyCollider.offset;
        Vector2 crouchMultiplier = new Vector2(1, crouchColliderMultiplier);
        crouchColliderSize = bodyColliderSize * crouchMultiplier;
        crouchColliderOffset = bodyColliderOffset * crouchMultiplier;
    }

    void FixedUpdate()
    {
        PhysicsCheck();

        GroundMovement();
        MidAirMovement();
    }

    void PhysicsCheck()
    {
        isOnGround = false;

        RaycastHit2D leftCheck = Raycast(new Vector2(-bodyColliderSize.x, 0), Vector2.down, skinWidth, groundLayer);
        RaycastHit2D rightCheck = Raycast(new Vector2(bodyColliderSize.x, 0), Vector2.down, skinWidth, groundLayer);

        if (leftCheck || rightCheck)
            isOnGround = true;

    }

    void GroundMovement()
    {
        //趴下时移动则站立

        float velocityX = speed * playerInput.horizontal;

        rb.velocity = new Vector2(velocityX, rb.velocity.y);

    }

    void MidAirMovement()
    {
        if (playerInput.jumpPressed)
        {
            isOnGround = false;
            isJumping = true;

            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    RaycastHit2D Raycast(Vector2 origin, Vector2 dir, float distance, LayerMask layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, distance, layer);

        Color color = hit ? Color.red : Color.green;
        Debug.DrawLine((Vector2)transform.position + origin, (Vector2)transform.position + origin + dir * distance, color);

        return hit;
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawLine(new Vector2(.3f, 0), new Vector2(.3f, 0) + Vector2.down * skinWidth);
    }
}
