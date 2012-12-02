using UnityEngine;
using System.Collections;

public class FinishScript : BaseScript {
	protected override string GetTag(){ return "End"; }
	
	void FixedUpdate(){
		transform.Rotate(new Vector3(0,0.4f,0));
	}
}
