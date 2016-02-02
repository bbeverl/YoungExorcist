using UnityEngine;
using System.Collections;

public class MoveToFloor : MonoBehaviour {

	public int floorNum;
    void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject == GameManager.Instance.Player) {
			GameManager.Instance.MoveToFloor(floorNum);
        }
    }
}
