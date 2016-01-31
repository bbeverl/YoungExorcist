using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    public AudioSource[] audioSource;

    public static int WalkSound = 0;
    public static int SelectEnemy = 1;

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
