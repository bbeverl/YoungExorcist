using UnityEngine;
using System.Collections;

public class PlayerSound : MonoBehaviour {

	public AudioSource[] audioClip;


	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	

		//PlaySound (index of array on player);


	}

	
	void PlaySound (int clip){
		
		audio.source = audioClip[clip];
		audio.Play ();
		
	}


}
