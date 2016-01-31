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
                // Find the Player GO
            }
            return player;
        }
    }

    private RitualNodeController ritualController;
    public RitualNodeController RitualController
    {
        get; set;
    }
  
    public bool StartRitual (GameObject target)
    {
        if(RitualController != null && !RitualController.IsRitualRunning) {            
            RitualController.StartRitual ();
        }

        return false;
    }



}
