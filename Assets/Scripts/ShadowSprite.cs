using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class ShadowSprite : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		sprite.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		sprite.receiveShadows = true;
	}
}
