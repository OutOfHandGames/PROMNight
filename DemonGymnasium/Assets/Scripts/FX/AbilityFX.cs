using UnityEngine;
using System.Collections;

public class AbilityFX : MonoBehaviour {

	public float offsetX;
	public float offsetY;
	public float animationTime;

	private Animator animator;
	private SpriteRenderer rend_base;
	private SpriteRenderer rend_particle;
	private AudioSource AS;

	public void Awake(){
		AS = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
		rend_base = GetComponent<SpriteRenderer> ();
		rend_particle = transform.Find ("Particles").gameObject.GetComponent<SpriteRenderer>();
	}

	public void SetupAndPlay (Transform targetTransform){
		Vector3 targetPos = targetTransform.position;
		targetPos = new Vector3 (targetPos.x + offsetX, targetPos.y + offsetY, targetPos.z);
		transform.position = targetPos;
		Enable ();
	}

	public void Enable(){
		AS.Stop ();
		AS.Play ();
		animator.enabled = true;
		rend_base.enabled = true;
		rend_particle.enabled = true;
		Invoke ("Disable", animationTime);
	}

	public void Disable (){
		animator.enabled = false;
		rend_base.enabled = false;
		rend_particle.enabled = false;
		Destroy ();
	}

	public void Destroy(){
		Destroy (gameObject);
	}



}
