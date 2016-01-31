using UnityEngine;
using System.Collections;

public class TopDownCharacterController : MonoBehaviour {

    private float moveSpeed = 3;

    public Rigidbody myRigidbody;

    void Start ()
    {
        if(myRigidbody == null) {
            myRigidbody = GetComponent<Rigidbody> ();
        }
    }

    void FixedUpdate()
    {
        if(GameManager.Instance.RitualController.IsRitualRunning == false) {
            Vector3 inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            //myRigidbody.MovePosition((myRigidbody.position + AxisInput) * Time.fixedDeltaTime * Speed);
            transform.Translate(inputMovement*Time.deltaTime*moveSpeed, Space.World);
        }
    }
}
