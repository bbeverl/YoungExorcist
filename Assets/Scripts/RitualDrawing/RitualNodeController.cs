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

    [SerializeField]
    private float ritualTimeout = 2f;
    [SerializeField]
    private float ritualFinishDelay = 0.5f;

    [SerializeField]
    private GameObject nodeParent;

    public bool IsRitualRunning {
        get; private set;
    }

    bool drawStarted = false;

    private void Awake ()
    {        
        GameManager.Instance.RitualController = this;

        if(lineDrawer == null) {            
            lineDrawer = GetComponentInChildren<DrawLine>();
        }
        if(lineDrawer != null) {
            lineDrawer.OnLinePointAdded += OnLinePointAdded;
            lineDrawer.OnLineDrawingStopped += OnDrawingStopped;
        }

        if(ritualNodes.Count < 1) {
            RitualNode[] nodes = GetComponentsInChildren<RitualNode>();
            ritualNodes.AddRange(nodes);
        }

        nodeParent.SetActive (false);
    }
        
	private void OnDestroy () 
	{
        GameManager.Instance.RitualController = null;

		if(lineDrawer != null) {
			lineDrawer.OnLinePointAdded -= OnLinePointAdded;
			lineDrawer.OnLineDrawingStopped -= OnDrawingStopped;
		}

        StopAllCoroutines ();
        ResetRitualEvents ();
	}

    public void StartRitual ()
    {
        IsRitualRunning = true;

        StartCoroutine (WaitAndEnable ());
    }

    private System.Collections.IEnumerator WaitAndEnable ()
    {                        
        yield return new WaitForSeconds(0.1f);
        nodeParent.SetActive(true);
        StartCoroutine(TimeoutRitual ());
    }
		
    private System.Collections.IEnumerator TimeoutRitual ()
    {
        yield return new WaitForSeconds(ritualTimeout);

        if(drawStarted == false) {
			StartCoroutine(FadeOutRitual ());
        }
    }

    private void OnLinePointAdded (Vector3 previousPoint, Vector3 newPoint)
    {
        StopCoroutine(TimeoutRitual ());

        drawStarted = true;

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

	private void OnDrawingStopped ()
	{
        if(this.gameObject.activeSelf && this.enabled) {
		    StartCoroutine(FinishRitual());
        }
	}

	private System.Collections.IEnumerator FinishRitual ()
	{
		yield return new WaitForSeconds(ritualFinishDelay);
        
       StartCoroutine(FadeOutRitual ());

       // ResetRitualEvents ();
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

        for (int i = 0; i < rendererObjects.Length; i++)
        {
            Color newColor = rendererObjects[i].material.color;
            newColor.a = 1;         
            rendererObjects[i].material.SetColor("_Color", newColor) ; 
        }

        ResetRitualEvents ();
	}

    void ResetRitualEvents ()
    {
        lineDrawer.ResetLines ();

        for (int i = 0, ritualNodesCount = ritualNodes.Count; i < ritualNodesCount; i++) {
            ritualNodes [i].ResetHit ();
        }

        hitSequence.Clear ();

        drawStarted = false;
        IsRitualRunning = false;

        nodeParent.SetActive(false);

    }

}
