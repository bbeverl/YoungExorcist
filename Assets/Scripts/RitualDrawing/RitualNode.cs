using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider)) ]
public class RitualNode : MonoBehaviour {

	public bool IsHit {
		get; private set;
	}

	public bool DetectHit (Vector3 originPoint, Vector3 endPoint)
	{
		Collider collider = gameObject.GetComponent<Collider>();
		if(collider.bounds.Contains(originPoint) || collider.bounds.Contains(endPoint)) {
			return true;
		}

		RaycastHit hitInfo;
		if(Physics.Raycast(endPoint, endPoint - originPoint,out hitInfo, (endPoint - originPoint).magnitude)) {
			if(hitInfo.collider.gameObject == this.gameObject) {
				return true;
			}
		}
		return false;
	}

	public void Hit ()
	{
		IsHit = true;

		Renderer renderer = this.GetComponent<Renderer>();
		if(renderer != null) {
			renderer.material.color = Color.blue;
		}
	}

	public void ResetHit ()
	{
		IsHit = false;

		Renderer renderer = this.GetComponent<Renderer>();
		if(renderer != null) {
			renderer.material.color = Color.white;
		}
	}

}
