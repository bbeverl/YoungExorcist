using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {

    private float moveSpeed = 3;
	private SpriteRenderer sprite;
	private Light lantern;

	void Start ()
	{
		if (sprite == null) {
			sprite = GetComponentInChildren<SpriteRenderer> ();
		}
		if(lantern == null) {
			lantern = GetComponentInChildren<Light> ();
		}
	}

    void FixedUpdate()
    {
        if(GameManager.Instance.RitualController.IsRitualRunning == false) {
            Vector3 inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            Vector3 translation = inputMovement*Time.deltaTime*moveSpeed;
            transform.Translate(translation, Space.World);

            if(translation.magnitude > 0) {
                GameManager.Instance.PlaySound(AudioController.WalkSound);
            }

			if(inputMovement.x < 0) {
				if(sprite.flipX == true) {
					sprite.flipX = false;
					lantern.transform.localPosition = new Vector3(lantern.transform.localPosition.x * -1, lantern.transform.localPosition.y, lantern.transform.localPosition.z);
				}
                    
			} else if(inputMovement.x > 0) {
				if(sprite.flipX == false) {
					sprite.flipX = true;
					lantern.transform.localPosition = new Vector3(lantern.transform.localPosition.x * -1, lantern.transform.localPosition.y, lantern.transform.localPosition.z);
				}
                    
			}                
        }
            
			
    }
}
