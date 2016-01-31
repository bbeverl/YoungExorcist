using UnityEngine;
using System.Collections;
using System.Linq;

public class WaterCreatureRitualStarter : CreatureRitualStarter {

	// This is the water sequance, fire is weak to water.
	private int[] mySequence = RitualSequences.WaterSequence;

	#region implemented abstract members of CreatureRitualStarter
	protected override bool CheckRitualSequence (int[] sequence)
	{
		
		if(mySequence.SequenceEqual(sequence)) {
			return true;
		}

		return false;
	}

	#endregion


}
