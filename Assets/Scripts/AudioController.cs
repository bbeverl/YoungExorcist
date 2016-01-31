using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    public AudioSource[] audioSource;

    public static int WalkSound = 0;
    public static int SelectEnemy = 1;
	public static int CastSpell = 2;
	public static int FailSpell = 3;
	public static int MonsterAttacks = 4;
	public static int Stairs = 5;
	public static int MonsterDefeat = 6;



    void Start ()
    {
        if(audioSource == null || audioSource.Length < 1)
        {
            audioSource = GetComponentsInChildren<AudioSource> ();
        }
    }
        
    public bool PlaySound (int clip){
        if(audioSource[clip].isPlaying == false) {
            audioSource[clip].Play ();
            return true;
        }

        return false;
    }
}
