using UnityEngine;
using System.Collections;

public class BodyController : MonoBehaviour {
	
	public GameObject nextBody;
	private BodyController nextController;
	
	// Body Movement Variables
	private static int maxPositions = 7;
	private int currentPosition = 0;
	Queue lastPositions = new Queue(maxPositions);
	
	public void SetPosition(Vector3 newPosition){
		SaveCurrentPosition(transform.position);
		
		transform.LookAt(newPosition, new Vector3(1,1,0));
		transform.Rotate (new Vector3(1,0,0), 90);
		transform.position = newPosition;
		
		// Chain the call to the rest of the body
		if(nextBody != null) GetNextController().SetPosition(GetTailPosition());
	}
	
	private void SaveCurrentPosition(Vector3 position){
		lastPositions.Enqueue(position);
	}
	
	private Vector3 GetTailPosition(){
		return lastPositions.Count < maxPositions ? transform.position : (Vector3) lastPositions.Dequeue();
	}
	
	private BodyController GetNextController(){
       if(nextController == null) {
           nextController = (BodyController) nextBody.GetComponent(typeof(BodyController));
       }
       return nextController;
    }
}
