using UnityEngine;
using System.Collections;

// Information about getting sprites to cast / receive shadows found here:
// http://forum.unity3d.com/threads/sprite-receive-shadow.357705/#post-2319797

[RequireComponent (typeof(SpriteRenderer))]
public class ShadowSprite : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		sprite.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		sprite.receiveShadows = true;
	}
}
