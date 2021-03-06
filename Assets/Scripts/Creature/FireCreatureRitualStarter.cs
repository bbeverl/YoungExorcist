﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class FireCreatureRitualStarter : CreatureRitualStarter {

	// This is the water sequance, fire is weak to water.
    private int[] mySequence = RitualSequences.FireSequence;

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
