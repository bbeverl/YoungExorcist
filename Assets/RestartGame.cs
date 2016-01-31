using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {

	void Awake ()
	{
		GameManager.Instance.RestartButton = this;
		this.gameObject.SetActive(false);
	}

	public void DoRestartGame ()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}
}
