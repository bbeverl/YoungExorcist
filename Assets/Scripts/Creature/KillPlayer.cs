using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	void OnCollisionEnter(Collision collision) 
	{
		if(collision.gameObject == GameManager.Instance.Player) {
			GameManager.Instance.KillPlayer();
		}
	}
}
