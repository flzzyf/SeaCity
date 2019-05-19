using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
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
}
