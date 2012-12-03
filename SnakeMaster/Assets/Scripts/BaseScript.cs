using UnityEngine;
using System.Collections;

public abstract class BaseScript : MonoBehaviour {
	
	void Start()  { gameObject.tag = GetTag(); }
	
	// This is a hack cause the snake head cant have a rigidbody rightnow.
	private int pos = 1;
	void Update() {
		transform.position += new Vector3(0,0.001f,0) * pos;
		pos *= -1;
	}
	
	protected abstract string GetTag();
}
