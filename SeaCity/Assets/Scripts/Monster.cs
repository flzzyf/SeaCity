using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
	public float speed = 1f;

	public SpriteRenderer gfx;
	Animator animator;

	Rigidbody2D rb;

	public Transform target;

	public bool facingRight;

	void Start()
	{
		animator = gfx.GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();

		InitHp();
	}

	void Update()
	{
		//如果没有目标就朝前走，前面没路就转头，看到玩家就设为目标并攻击
		//有目标则朝目标方向移动，除非没路

		int moveDir = facingRight ? 1 : -1;

		rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);

		animator.SetFloat("Speed", 1);


		

		if(Input.GetKeyDown("z"))
		{
			Attack();
		}

		if(Input.GetKeyDown("x"))
		{
			Death();
		}
	}

	void Flip()
	{
		gfx.flipX = !gfx.flipX;

		facingRight = !facingRight;
	}

	void Attack()
	{
		animator.SetTrigger("Attack");
	}

	public void AttackAnimEvent()
	{

	}

	#region 生命值
	public int hpMax = 5;
	int hpCurrent;

	void InitHp()
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

	void TakeDamage(int amount)
	{
		ModifyHp(-amount);
	}

	void Death()
	{
		animator.SetTrigger("Death");
	}

	public void DieAnimEvent()
	{
		Destroy(gameObject);
	}
	#endregion
}
