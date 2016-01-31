using UnityEngine;
using System.Collections;

public class GameManager : ScriptableObject {

	private static GameManager instance;
    public static GameManager Instance {
        get {
            if(instance == null) {
                instance = ScriptableObject.CreateInstance<GameManager>();
            }

            return instance;
        }
    }

    void OnDestroy ()
    {
        instance = null;
    }

    private GameObject player;
    public GameObject Player {
        get {
            if(player == null) {
				player = GameObject.FindGameObjectWithTag("Player");
            }
            return player;
        }
    }

    private RitualNodeController ritualController;
    public RitualNodeController RitualController
    {
		get {
			if(ritualController == null) {
				ritualController = FindObjectOfType<RitualNodeController>();
			}
			return ritualController;
		}
    }

    private AudioController audioController;
    public AudioController AudioController
    {
        get {
            if(audioController == null) {
                audioController = FindObjectOfType<AudioController>();
            }
            return audioController;
        }
    }

	public RestartGame RestartButton {
		get; set;
	}
  
	public bool StartRitual (RitualStarter target)
    {
		
        if(Player != null && RitualController != null && !RitualController.IsRitualRunning) {            
            RitualController.StartRitual (target);
        }

        return false;
    }

	public void KillPlayer ()
	{
		if(Player != null)
		{
			Destroy(Player);
		}
		// End Ritual
		if(RitualController != null && RitualController.IsRitualRunning) {
			RitualController.AbandonRitual ();
		}
		// End Game
		if(RestartButton != null) {
			RestartButton.gameObject.SetActive(true);
		}
	}

    public bool PlaySound (int clipNum)
    {
        if(GameManager.Instance.AudioController != null) {
            return GameManager.Instance.AudioController.PlaySound(AudioController.WalkSound);
        }
        return false;
    }

}
