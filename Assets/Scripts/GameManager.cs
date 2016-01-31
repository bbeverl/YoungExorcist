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
  
	public bool StartRitual (RitualStarter target)
    {
        if(RitualController != null && !RitualController.IsRitualRunning) {            
            RitualController.StartRitual (target);
        }

        return false;
    }



}
