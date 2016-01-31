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

    public void ChangeTextForWin ()
    {
        UnityEngine.UI.Text text = GetComponentInChildren<UnityEngine.UI.Text>();
        text.text = "You Won! Press to restart";
    }
}
