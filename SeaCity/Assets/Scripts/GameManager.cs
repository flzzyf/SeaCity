using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //游戏进行中
    public static bool isGaming;

	//游戏暂停
	public static bool isPaused = false;

	public static void GamePause()
	{
		isPaused = true;

		Time.timeScale = 0;
	}

	public static void GameContinue()
	{
		isPaused = false;

		Time.timeScale = 1;
	}

    void Start()
    {
        isGaming = true;
    }

    public void Restart()
	{
		SceneFader.instance.ReloadScene();
	}
}
