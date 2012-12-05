using UnityEngine;
using System.Collections;

public class LevelsMenuManagerScript : MonoBehaviour {

	public Font font;
	Texture2D frame;
	public Texture starTexture;
	public Texture aTexture;
	
	void Start(){
		frame = Resources.Load("frame_1") as Texture2D;
	}
	
	void Update(){
//		frame = Resources.Load("frame_1") as Texture2D;
		if(Input.GetButton("Jump")) Application.LoadLevel(1);
	}
	
	void OnGUI(){
		setTitle();
		setSubtitle();
		setMenu();
	}
	
	private void setTitle() {
		var style = getTitleStyle(Color.white);
		GUI.Label(new Rect(20, 50, 400, 100), "Snake Master", style);
	}

	private void setSubtitle() {
		var style = getTitleStyle(Color.white);
		style.fontSize = 30;
		var buttonStyle = getButtonStyle();
		GUI.Label(new Rect(350, 65, 300, 100), "Levels", style);
		if(GUI.Button(new Rect(Screen.width-150, 65, 100, 40), "Back", buttonStyle)) {
			Application.LoadLevel("SplashScene");
		}
	}
	
	private void setMenu() {
		int left = 100;
		int top = 160;
		int leftMargin = 200;
		int topMargin = 220;
		int menuTop = top;
		int leftCounter = 0;
		int topCounter = 0;

		var boxStyle = new GUIStyle(GUI.skin.box);
		boxStyle.font = font;
		boxStyle.fontSize = 15;
		boxStyle.normal.textColor = Color.white;
//		boxStyle.normal.background = frame;
		
		var buttonStyle = getButtonStyle();
		
		var labelStyle = new GUIStyle(GUI.skin.label);
		labelStyle.font = font;
		labelStyle.fontSize = 20;
		labelStyle.normal.textColor = Color.white;
		labelStyle.alignment = TextAnchor.MiddleCenter;
		
		for(int i = 0; i < GameController.numLevels() ; i++) {
			int menuLeft = left+leftMargin*leftCounter++;
			if(menuLeft > Screen.width - 200) {
				menuLeft = left;
				menuTop += topMargin;
				leftCounter = 1;
			}
			
			GUI.Box(new Rect(menuLeft, menuTop, 150, 180), "", boxStyle);
			if(GameController.HasEnoughStarts(i+1)) {
				if(GUI.Button(new Rect(menuLeft+25, menuTop+110, 100, 50), "Play", buttonStyle)) {
					Application.LoadLevel("Level"+(i+1));
				}
				int inMapTotalStars = GameController.inMapLevelStars(i+1);
				int inMapPickedStars = GameController.inMapPickedLevelStars(i+1);
				if(inMapTotalStars > inMapPickedStars) {
					GUI.Label(new Rect(menuLeft+30, menuTop+60, 80, 40), inMapPickedStars+"/"+inMapTotalStars, labelStyle);					
					GUI.DrawTexture(new Rect(menuLeft+25, menuTop+67, 20, 20), starTexture, ScaleMode.ScaleToFit, true, 1.12f);
					GUI.DrawTexture(new Rect(menuLeft+100, menuTop+67, 20, 20), starTexture, ScaleMode.ScaleToFit, true, 1.12f);
				} else {
					GUI.DrawTexture(new Rect(menuLeft+55, menuTop+45, 40, 40), starTexture, ScaleMode.ScaleToFit, true, 1.12f);					
					labelStyle.fontSize = 15;
					GUI.Label(new Rect(menuLeft+25, menuTop+80, 100, 40), "Mastered", labelStyle);					
				}
			} else {
				labelStyle.normal.textColor = Color.grey;
				GUI.DrawTexture(new Rect(menuLeft+50, menuTop+55, 50, 65), aTexture, ScaleMode.ScaleToFit, true, 0.76f);
				int leftStars = GameController.leftLevelStars(i+1);
				int totalStars = GameController.totalLevelStars(i+1);
				GUI.DrawTexture(new Rect(menuLeft+80, menuTop+150, 25, 25), starTexture, ScaleMode.ScaleToFit, true, 1.12f);
				GUI.Label(new Rect(menuLeft+15, menuTop+120, 120, 40), "unlock", labelStyle);
				GUI.Label(new Rect(menuLeft+5, menuTop+145, 120, 40), ""+(totalStars-leftStars), labelStyle);
			}
			labelStyle.fontSize = 20;
			GUI.Label(new Rect(menuLeft+25, menuTop+10, 100, 40), "Level "+(i+1), labelStyle);
		}
	}	
	
	private GUIStyle getTitleStyle(Color color){
		var style = new GUIStyle();
		style.font = font;
		style.fontSize = 45;
		style.normal.textColor = color;
		style.alignment = TextAnchor.UpperCenter;
		return style;
	}

	private GUIStyle getButtonStyle(){
		var buttonStyle = new GUIStyle(GUI.skin.button);
		buttonStyle.font = font;
		buttonStyle.fontSize = 20;
		buttonStyle.normal.textColor = Color.white;
		buttonStyle.hover.textColor = Color.green;
		buttonStyle.active.textColor = Color.red;
		return buttonStyle;
	}
}