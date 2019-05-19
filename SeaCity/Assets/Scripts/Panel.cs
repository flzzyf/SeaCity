using UnityEngine;

public class Panel : MonoBehaviour
{
    public virtual void Show()
	{
		gameObject.SetActive(true);
	}

	public virtual void Close()
	{
		gameObject.SetActive(false);
	}
}
