using UnityEngine;
using System.Collections;

public class LevelsMenuManagerScript : MonoBehaviour {

	public Font font;
	public Texture starTexture;
	
	void Update(){
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
		GUI.Label(new Rect(350, 65, 300, 100), "Levels", style);
	}
	
	private void setMenu() {
		int left = 100;
		int top = 160;
		int leftMargin = 200;
		int topMargin = 230;
		int menuTop = top;
		int leftCounter = 0;
		int topCounter = 0;
		for(int i = 0; i < 20; i++) {
			int menuLeft = left+leftMargin*leftCounter++;
			if(menuLeft > Screen.width - 200) {
				menuLeft = left;
				menuTop += topMargin;
				leftCounter = 1;
			}
			
			GUIContent content = new GUIContent("Level " + (i+1), starTexture);
				
			if(GUI.Button(new Rect(menuLeft, menuTop, 150, 180), content)) {
				Application.LoadLevel("Level"+(i+1));
			}
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
}