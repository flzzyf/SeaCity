using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	protected virtual void Start()
	{
        bodyCollider = GetComponent<BoxCollider2D>();

	}

	protected virtual void Update()
	{
		GroundCheck();
	}

	#region 生命值
	public int hpMax = 5;
	protected int hpCurrent;

	protected void InitHp()
	{
		SetHp(hpMax);
	}

	void SetHp(int amount)
	{
		hpCurrent = amount;
	}
	void ModifyHp(int amount)
	{
		hpCurrent += amount;

		if (hpCurrent <= 0)
		{
			Death();
		}
	}

	public virtual void TakeDamage(int amount)
	{
		ModifyHp(-amount);
	}

	public virtual void Death()
	{
	}

	public void DieAnimEvent()
	{
		Destroy(gameObject);
	}
	#endregion

	#region 朝向

	public bool facingRight;

	public int facingDir { get { return facingRight ? 1 : -1; } }

	public void Flip()
	{
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;

		facingRight = !facingRight;
	}
	#endregion

	#region  地面判定
	public bool isOnGround;

	public LayerMask groundLayer;

	const float skinWidth = .05f;

	BoxCollider2D bodyCollider;

	//判断是否在地面上
	void GroundCheck()
	{
		RaycastHit2D leftCheck = Utils.Raycast((Vector2)transform.position + new Vector2(-bodyCollider.size.x / 2, 0), Vector2.down, skinWidth, groundLayer);
		RaycastHit2D rightCheck = Utils.Raycast((Vector2)transform.position + new Vector2(bodyCollider.size.x / 2, 0), Vector2.down, skinWidth, groundLayer);

		isOnGround = leftCheck || rightCheck;
	}
	#endregion
}
