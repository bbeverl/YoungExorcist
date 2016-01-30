using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RitualNodeController : MonoBehaviour {

	[SerializeField]
	private DrawLine lineDrawer = null;

	[SerializeField]
	private List<RitualNode> ritualNodes = new List<RitualNode>();

	[SerializeField]
	private List<int> hitSequence = new List<int> ();

	private void OnEnable ()
    {
        StartCoroutine(TimeoutRitual ());

		if(ritualNodes.Count < 1) {
			RitualNode[] nodes = GetComponentsInChildren<RitualNode>();
			ritualNodes.AddRange(nodes);
		}

		if(lineDrawer == null) {			
			lineDrawer = GetComponentInChildren<DrawLine>();
		}
		if(lineDrawer != null) {
			lineDrawer.OnLinePointAdded += OnLinePointAdded;
			lineDrawer.OnLineDrawingStopped += OnDrawingStopped;
		}
	}

	private void OnDisable () 
	{
		if(lineDrawer != null) {
			lineDrawer.OnLinePointAdded -= OnLinePointAdded;
			lineDrawer.OnLineDrawingStopped -= OnDrawingStopped;
		}

        ResetRitualEvents ();
	}
	
	private void OnLinePointAdded (Vector3 previousPoint, Vector3 newPoint)
	{
        StopCoroutine(TimeoutRitual ());

		for (int i = 0, ritualNodesCount = ritualNodes.Count; i < ritualNodesCount; i++) {
			RitualNode node = ritualNodes [i];

			if (node.DetectHit (previousPoint, newPoint)) {
				node.Hit ();
				if(hitSequence.Count == 0 || hitSequence[hitSequence.Count - 1] != i) {
					hitSequence.Add(i);
				}
			}
		}
	}

    private System.Collections.IEnumerator TimeoutRitual ()
    {
        yield return new WaitForSeconds(2);

        if(hitSequence.Count < 1) {
            this.gameObject.SetActive(false);
        }
    }

	private void OnDrawingStopped ()
	{
        if(this.gameObject.activeSelf && this.enabled) {
		    StartCoroutine(FinishRitual());
        }
	}

	private System.Collections.IEnumerator FinishRitual ()
	{
		yield return new WaitForSeconds(1);
        
        this.gameObject.SetActive(false);
	}

    void ResetRitualEvents ()
    {
        lineDrawer.ResetLines ();
        for (int i = 0, ritualNodesCount = ritualNodes.Count; i < ritualNodesCount; i++) {
            ritualNodes [i].ResetHit ();
        }
        hitSequence.Clear ();
    }
}
