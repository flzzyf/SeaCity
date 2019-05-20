using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1;
    //走出平台后仍能跳跃的时间
    public float coyoteTime = .05f;

    public float jumpForce = 6f;

    public bool isJumping;
    public bool isCrouching;

    PlayerInput playerInput;
	Player player;
    Rigidbody2D rb;

    void Start()
    {
		player = GetComponent<Player>();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
		//判断是否在地面上
		if (isJumping && player.isOnGround)
		{
			Land();
		}

		GroundMovement();

		if (playerInput.jumpPressed && player.isOnGround)
		{
			Jump();

		}
	}

	#region 跳跃判定


	//跳跃
	void Jump()
	{
		isJumping = true;

		player.animator.SetBool("Jumping", isJumping);

		rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
	}
	//着陆
	void Land()
	{
		isJumping = false;

		player.animator.SetBool("Jumping", isJumping);
	}

	#endregion

	void GroundMovement()
    {
		//播放动画
		player.animator.SetFloat("Speed", Mathf.Abs(playerInput.horizontal));

		//趴下时移动则站立

		float velocityX = speed * playerInput.horizontal;

		rb.velocity = new Vector2(velocityX, rb.velocity.y);

		//改变朝向
		if (velocityX * player.facingDir < 0f)
			player.Flip();

	}

	private void OnDrawGizmosSelected()
    {
		//Gizmos.DrawLine((Vector2)transform.position + new Vector2(-bodyColliderSize.x / 2, 0), (Vector2)transform.position + new Vector2(-bodyColliderSize.x / 2, 0) + Vector2.down * skinWidth);
		//Gizmos.DrawLine((Vector2)transform.position + new Vector2(bodyColliderSize.x / 2, 0), (Vector2)transform.position + new Vector2(-bodyColliderSize.x / 2, 0) + Vector2.down * skinWidth);

	}
}
