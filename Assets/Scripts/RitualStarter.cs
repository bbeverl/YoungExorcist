using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider)) ]
public abstract class RitualStarter : MonoBehaviour {

	protected virtual void OnMouseDown ()
    {
        GameManager.Instance.StartRitual(this);

    }
		
	public abstract void HandleRitualFinished (int[] sequence);
}
