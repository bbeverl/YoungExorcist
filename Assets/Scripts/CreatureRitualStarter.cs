using UnityEngine;
using System.Collections;

public abstract class CreatureRitualStarter : RitualStarter {

	protected abstract bool CheckRitualSequence (int[] sequence);

	#region implemented abstract members of RitualStarter
	public override void HandleRitualFinished (int[] sequence)
	{
		if(CheckRitualSequence (sequence)) {
			Destroy(this.gameObject);
		}
	}
	#endregion
	
}
