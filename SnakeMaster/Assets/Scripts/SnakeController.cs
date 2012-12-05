using UnityEngine;
using System.Collections;

public class SnakeController : MonoBehaviour {
	
	private GameController gameController;
	
	// Rest of the body
	public  GameObject body;
	private BodyController bodyController;
	
	// Rotation Variables
	public  int rotationSpeed    = 100;
	private int currentDirection = 1;
	
	// Movement Variables
	private float lastChange  = 0f;
	private  float deltaChange = 0.2f;
	
	// Body Movement Variables
	private static int maxPositions = 7;
	private int currentPosition = 0;
	Queue lastPositions = new Queue(maxPositions);
	
	
	void Start(){
		gameController = (GameController) GameObject.FindWithTag("GameController")
			 			 			  			.GetComponent(typeof(GameController));
	}
	
	// Update is called once per frame
	void Update() {
		SaveCurrentPosition(transform.position);
		
		// Move the Snake Head
		float rotation = -rotationSpeed * Time.deltaTime * currentDirection;
		transform.parent.RotateAround(transform.parent.position, Vector3.forward, rotation);
		
		// Move the rest of the Body
		GetBodyController().SetPosition(GetTailPosition());
		
		// Change Direction if necesary
		if(Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(Time.time - lastChange) > deltaChange){
			lastChange = Time.time;
			transform.parent.Translate(new Vector3(0,1,0) * 3 * currentDirection);
			transform.Translate(new Vector3(0,-1,0) * 3 * currentDirection);
			currentDirection *= -1;
		}
		
		CheckScreenBoundaries();
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
	
	int outOfScreen = 0; // Little Hack for short time of no visibility
	void CheckScreenBoundaries(){
		if(!renderer.isVisible){
			if(outOfScreen++ > 5) gameController.ResetLevel();
		}
	}
	
	void OnTriggerEnter (Collider other) {
    	tag = other.gameObject.tag;
		switch(tag){
		case "Star":
			Destroy(other.gameObject);
			gameController.CollectStar();
			break;
		
		case "End":
			gameController.LoadNextLevel();
			break;
			
		default:
			gameController.ResetLevel();
			break;
		}
	}
}
