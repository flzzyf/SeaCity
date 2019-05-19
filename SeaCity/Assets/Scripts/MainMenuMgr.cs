using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMgr : MonoBehaviour
{
	public GameObject panel_Setting;

	//各种按钮点击事件
    public void OnButtonStartClick()
	{
		SceneFader.instance.FadeToScene("Game");
	}

	public void OnButtonSettingClick()
	{
		panel_Setting.SetActive(true);
	}

	public void OnButtonQuitClick()
	{
		Application.Quit();
	}
}
