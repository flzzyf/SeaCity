using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
	public float speed = 1f;

	public SpriteRenderer gfx;
	Animator animator;

	Rigidbody2D rb;

	public Transform target;


	public float eyeHeight = 1;
	public float eyeSight = 3;

	protected override void Start()
	{
		base.Start();

		animator = gfx.GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();

		InitHp();
	}

	protected override void Update()
	{
		base.Update();

		if (!isOnGround)
			return;

		//如果没有目标就朝前走，前面没路就转头，看到玩家就设为目标并攻击
		//有目标则朝目标方向移动，除非没路

		if(!attacking)
			rb.velocity = new Vector2(facingDir * speed, rb.velocity.y);

		animator.SetFloat("Speed", 1);

		if(Input.GetKeyDown("z"))
		{
			Attack();
		}

		if(Input.GetKeyDown("x"))
		{
			Death();
		}

		//搜索前方目标
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + transform.up * eyeHeight, transform.right * facingDir, attackRange);
		foreach (var item in hits)
		{
			if (item.collider.transform.parent != transform && item.collider.gameObject.tag == "Player")
			{
				Attack();
			}
		}

		//攻击冷却
		AttackCooling();

		if(!WalkableForward())
		{
			Flip();
		}
	}


	#region 攻击
	public int damage = 1;

	public float cooldown = 1;
	float currentCooldown;

	public float attackRange = 1f;

	public bool canAttack { get { return currentCooldown <= 0; } }

	//攻击中
	bool attacking;

	void Attack()
	{
		if(canAttack)
		{
			currentCooldown = cooldown;
		}
		else
		{
			return;
		}

		attacking = true;

		animator.SetTrigger("Attack");
	}

	public void AttackAnimEvent()
	{
		//搜索前方目标
		int facingDir = facingRight ? 1 : -1;
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + transform.up * eyeHeight, transform.right * facingDir, attackRange);
		foreach (var item in hits)
		{
			if (item.collider.transform.parent != transform && item.collider.gameObject.tag == "Player")
			{
				item.collider.gameObject.GetComponent<Character>().TakeDamage(damage);

				Utils.Push(item.collider.GetComponent<Rigidbody2D>(), new Vector2(facingDir, 0.2f).normalized, 8);
			}
		}

		attacking = false;
	}

	//攻击冷却
	void AttackCooling()
	{
		currentCooldown -= Time.deltaTime;
	}

	#endregion

	public override void Death()
	{
		base.Death();

		animator.SetTrigger("Death");
	}

	public float pathFinder_x;
	public float pathFinder_length = 1f;

	bool WalkableForward()
	{
		bool walkable = false;
		//判断前方地面
		foreach (var item in Physics2D.RaycastAll(transform.position + transform.right * pathFinder_x * facingDir + transform.up * .1f, -transform.up, pathFinder_length))
		{
			if (item.collider.transform.parent != transform && item.collider.gameObject.tag == "Ground")
			{
				walkable = true;
				break;
			}
		}

		//判断前方墙壁
		foreach (var item in Physics2D.RaycastAll(transform.position + transform.right * pathFinder_x * facingDir + transform.up * 1, transform.right * facingDir, pathFinder_length))
		{
			if (item.collider.transform.parent != transform && item.collider.gameObject.tag == "Ground")
			{
				walkable = false;
				break;
			}
		}

		return walkable;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawLine(transform.position + transform.up * eyeHeight,
			transform.position + transform.up * eyeHeight + transform.right * attackRange);
	}
}
