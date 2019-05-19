using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
	public float introAnimDuration = 5f;

    void Start()
    {
		StartCoroutine(DelayLoadScene());
    }

	IEnumerator DelayLoadScene()
	{
		yield return new WaitForSeconds(introAnimDuration);

		SceneFader.instance.FadeToScene("MainMenu");
	}

}
