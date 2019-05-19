using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_HpBar : Singleton<Panel_HpBar>
{
	public Image[] hearts;

	public Sprite[] heartSprite;

	public void SetHp(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			hearts[i].sprite = heartSprite[0];
		}

		for (int i = amount; i < 3; i++)
		{
			hearts[i].sprite = heartSprite[1];
		}
	}
}
