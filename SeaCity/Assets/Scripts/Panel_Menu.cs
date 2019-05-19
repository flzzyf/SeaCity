using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Menu : Panel
{
	public override void Show()
	{
		base.Show();

		GameManager.GamePause();
	}

	public void OnButtonContinueClick()
	{
		Close();

		GameManager.GameContinue();
	}

    public void OnButtonRestartClick()
	{
		SceneFader.instance.ReloadScene();

		GameManager.GameContinue();
	}

	public void OnButtonSettingClick()
	{

	}

	public void OnButtonHomeClick()
	{
		SceneFader.instance.FadeToScene("MainMenu");

		GameManager.GameContinue();
	}
}
