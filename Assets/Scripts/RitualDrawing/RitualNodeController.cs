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
			StartCoroutine(FadeOutRitual ());
            //this.gameObject.SetActive(false);
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
		yield return new WaitForSeconds(0.5f);
        
		StartCoroutine(FadeOutRitual ());
        //this.gameObject.SetActive(false);
	}

	float MaxAlpha(Renderer[] rendererObjects)
	{
		float maxAlpha = 0.0f; 

		foreach (Renderer item in rendererObjects)
		{
			maxAlpha = Mathf.Max (maxAlpha, item.material.color.a); 
		}
		return maxAlpha; 
	}

	private System.Collections.IEnumerator FadeOutRitual ()
	{
		float fadingOutTime = -0.5f;
		// log fading direction, then precalculate fading speed as a multiplier
		bool fadingOut = (fadingOutTime < 0.0f);
		float fadingOutSpeed = 1.0f / fadingOutTime; 

		// grab all child objects
		Renderer[] rendererObjects = GetComponentsInChildren<Renderer>(); 

		// make all objects visible
		for (int i = 0; i < rendererObjects.Length; i++)
		{
			rendererObjects[i].enabled = true;
		}

		// get current max alpha
		float alphaValue = MaxAlpha( rendererObjects);  


		// iterate to change alpha value 
		while ( (alphaValue >= 0.0f && fadingOut) || (alphaValue <= 1.0f && !fadingOut)) 
		{
			alphaValue += Time.deltaTime * fadingOutSpeed; 

			for (int i = 0; i < rendererObjects.Length; i++)
			{
				Color newColor = rendererObjects[i].material.color;
				newColor.a = Mathf.Min ( newColor.a, alphaValue ); 
				newColor.a = Mathf.Clamp (newColor.a, 0.0f, 1.0f); 				
				rendererObjects[i].material.SetColor("_Color", newColor) ; 
			}

			yield return null; 
		}

		this.gameObject.SetActive(false);
		for (int i = 0; i < rendererObjects.Length; i++)
		{
			Color newColor = rendererObjects[i].material.color;
			newColor.a = 1;			
			rendererObjects[i].material.SetColor("_Color", newColor) ; 
		}
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
