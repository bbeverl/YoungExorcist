using UnityEngine;
using System.Collections;

public class MoveToFloor2 : MonoBehaviour {

    void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject == GameManager.Instance.Player) {
            GameManager.Instance.MoveToFloor(2);
        }
    }
}
