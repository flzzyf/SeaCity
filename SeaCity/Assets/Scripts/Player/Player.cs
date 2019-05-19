using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	public Animator animator;

	public Panel_GameOver panel_GameOver;

	private void Start()
	{
		InitHp();
	}

	public override void TakeDamage(int amount)
	{
		base.TakeDamage(amount);

		Panel_HpBar.instance.SetHp(hpCurrent);
	}

	public override void Death()
	{
		base.Death();

		gameObject.SetActive(false);

		panel_GameOver.gameObject.SetActive(true);

		GameManager.GamePause();
	}
}
