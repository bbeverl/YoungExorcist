using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider)) ]
public class RitualStarter : MonoBehaviour {

	void OnMouseDown ()
    {
        GameManager.Instance.StartRitual(this.gameObject);
    }
}
