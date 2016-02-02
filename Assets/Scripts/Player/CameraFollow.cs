using UnityEngine;
using System.Collections;

// Original FollowCamera for 2D top down taken from here :
// http://answers.unity3d.com/questions/29183/2d-camera-smooth-follow.html
// https://gist.github.com/unity3diy/5aa0b098cb06b3ccbe47

public class CameraFollow : MonoBehaviour {

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;

    Vector3 targetPos;
    // Use this for initialization
    void Start () {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;
            posNoZ.y = target.transform.position.y;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 

            transform.position = Vector3.Lerp( transform.position, targetPos + offset, 0.25f);

        }
    }
}