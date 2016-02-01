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
			GameManager.Instance.PlaySound(1);

        }

        return false;
    }

	public void KillPlayer ()
	{
		if(Player != null)
		{
			Destroy(Player);
			GameManager.Instance.PlaySound(6);

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
            return GameManager.Instance.AudioController.PlaySound(clipNum);
        }
        return false;
    }

    private int creaturesDown = 0;
    public void CreatureDown (GameObject creature) {
        creaturesDown++;
        GameObject.Destroy(creature);

        if(creaturesDown > 2) {
            
            RestartButton.gameObject.SetActive(true);
            RestartButton.ChangeTextForWin ();
        }
    }

    public void MoveToFloor (int floorNum)
    {
        GameObject floorSpawn = null;
        if(floorNum == 1) {
            floorSpawn = GameObject.Find("FloorOneTeleport");
			Vector3 newCamPos = Camera.main.transform.position;
			newCamPos.z = 0;
			Camera.main.transform.position = newCamPos;
        } else if (floorNum == 2) {
            floorSpawn = GameObject.Find("FloorTwoTeleport");

			Vector3 newCamPos = Camera.main.transform.position;
			newCamPos.z = 20;
			Camera.main.transform.position = newCamPos;
        }
        if(floorSpawn == null) return;

        // Move Player to stairs
		Player.transform.position = floorSpawn.transform.position;
        // Move Camera
    }

}


