using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	Player player;
    PlayerInput playerInput;

	public float attackInputTime = 0.5f;

	public float attackPosY = 0.5f;
	public float attackRange = 0.5f;

	//攻击阶段
	int attackPhase;

	bool allowInput;

	void Start()
    {
		player = GetComponent<Player>();
        playerInput = GetComponent<PlayerInput>();
	}

	void FixedUpdate()
    {
        if(playerInput.attackPressed && (allowInput || attackPhase == 0))
		{
			Attack();
		}
    }

	void Attack()
	{
		attackPhase++;

		player.animator.SetInteger("Attack", attackPhase);

	}

	public void AttackAnimEvent()
	{
		//搜索前方目标
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + transform.up * attackPosY, transform.right * player.facingDir, attackRange);
		foreach (var item in hits)
		{
			if (item.collider.transform.parent != transform && item.collider.gameObject.tag == "Enemy")
			{
				print("击中");
				item.collider.gameObject.GetComponent<Character>().TakeDamage(1);

				Utils.Push(item.collider.GetComponent<Rigidbody2D>(), new Vector2(player.facingDir, 0.4f).normalized, 300);
			}
		}

		allowInput = true;
	}

	public void AttackOverTimeAnimEvent()
	{
		allowInput = false;

		AttackEnd();
	}

	public void AttackEnd()
	{
		attackPhase = 0;

		player.animator.SetInteger("Attack", attackPhase);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawLine(transform.position + transform.up * attackPosY,
			transform.position + transform.up * attackPosY + transform.right * attackRange);
	}
}
