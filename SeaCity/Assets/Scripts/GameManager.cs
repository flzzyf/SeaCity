using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //游戏进行中
    public static bool isGaming;

    void Start()
    {
        isGaming = true;
    }

    public void Restart()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
