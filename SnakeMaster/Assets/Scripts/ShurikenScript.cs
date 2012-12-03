using UnityEngine;
using System.Collections;

public class ShurikenScript : BaseScript {
	protected override string GetTag(){ return "Shuriken"; }
	
	public int direction = 1;
	public float xRange = 3;
	public float yRange = 0;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(new Vector3(0,5,0));
		
		float x = GetMove(xRange) * direction;
		float y = GetMove(yRange) * direction;
		
		transform.position += new Vector3(x, y, 0) * 0.01f;
	}
	
	float GetMove(float range){
		return range != 0 ? Mathf.PingPong(Time.timeSinceLevelLoad, 2 * range) - range : 0;
	}
}
