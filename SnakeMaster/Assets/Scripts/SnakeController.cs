using UnityEngine;
using System.Collections;

public class SnakeController : MonoBehaviour {
		
	// Rest of the body
	public  GameObject body;
	private BodyController bodyController;
	
	// Rotation Variables
	public  int rotationSpeed    = 100;
	private int currentDirection = 1;
	
	// Movement Variables
	private float lastChange  = 0f;
	public  float deltaChange = 0.5f;
	
	// Body Movement Variables
	private static int maxPositions = 7;
	private int currentPosition = 0;
	Queue lastPositions = new Queue(maxPositions);
	
	// Update is called once per frame
	void Update () {
		SaveCurrentPosition(transform.position);
		
		// Move the Snake Head
		float rotation = -rotationSpeed * Time.deltaTime * currentDirection;
		transform.parent.RotateAround(transform.parent.position, Vector3.forward, rotation);
		
		// Move the rest of the Body
		GetBodyController().SetPosition(GetTailPosition());
		
		// Change Direction if necesary
		if(Input.GetButton("Jump") && Mathf.Abs(Time.time - lastChange) > deltaChange){
			lastChange = Time.time;
			transform.parent.Translate(new Vector3(0,1,0) * 3 * currentDirection);
			transform.Translate(new Vector3(0,-1,0) * 3 * currentDirection);
			currentDirection *= -1;
		}
		
		CheckScreenBoundaries(transform.position);
	}
	
	private void SaveCurrentPosition(Vector3 position){
		lastPositions.Enqueue(position);
	}
	
	private Vector3 GetTailPosition(){
		return lastPositions.Count < maxPositions ? transform.position : (Vector3) lastPositions.Dequeue();
	}
	
	private BodyController GetBodyController(){
       if(bodyController == null) {
           bodyController = (BodyController) body.GetComponent(typeof(BodyController));
       }
       return bodyController;
    }
	
	void CheckScreenBoundaries(Vector3 pos){
		float cameraSize = (float) Camera.main.camera.orthographicSize;
		if(pos.y < -cameraSize || pos.y > cameraSize || 
		   pos.x < -cameraSize || pos.x > cameraSize) ResetLevel();
	}
	
	void OnTriggerEnter (Collider other) {
    	tag = other.gameObject.tag;
		switch(tag){
		case "Star":
			Destroy(other.gameObject);
			print ("hey star!"); break;
		
		case "Wall":
			ResetLevel();
			print ("hey wall!"); break;
		
		case "End":
			ResetLevel();
			print ("hey finish!!"); break;
		}
	}
	
	void ResetLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}
}
