using UnityEngine;
using System.Collections;

public class LockUI : MonoBehaviour {

	public int turnGenerated;

	public int turnsTilDestroyed;

	public Tile tileAttched;
	private Animator animator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Setup(Transform targetTransform){
		tileAttched = targetTransform.gameObject.GetComponent<Tile> ();
		Vector3 targetPos = targetTransform.position;
		transform.position = targetPos;
	}

	public void DestroySelf(){
		tileAttched.locked = false;
		Destroy (gameObject);
	}
}
