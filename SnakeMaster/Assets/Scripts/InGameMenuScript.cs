using UnityEngine;
using System.Collections;

public class InGameMenuScript : MonoBehaviour {
	
	public Font font;
	public bool isActive = false;
	
	void Update(){
		if(Input.GetKeyUp(KeyCode.P)) isActive = !isActive;
	}	
	
	void OnGUI(){
		print(isActive);
		if(isActive) setMenu();
	}

	private GUIStyle getSubtitleStyle(Color color){
		var style = new GUIStyle(GUI.skin.button);
		style.font = font;
		style.fontSize = 25;
		style.normal.textColor = color;
		style.hover.textColor = Color.green;
		style.active.textColor = Color.red;
		
	    Texture2D tex = new Texture2D(2, 2);
	    Color[] colors = new Color[4];
	    tex.SetPixels(colors);
	    tex.Apply();
	    style.normal.background = tex;
		style.hover.background = tex;
		style.active.background = tex;
		
		return style;
	}
	
	private void setMenu() {
		var style = getSubtitleStyle(Color.yellow);
		int left = (Screen.width/2)-130;
		int top = (Screen.height/2)-80;
		int menumargin = 50;
		
		GUI.Box(new Rect(left+20, top-50, 220, 250), "");
		
		if(GUI.Button(new Rect(left, top, 260, 40), "Resume", style)) {
			isActive = false;
		}
		
		if(GUI.Button(new Rect(left, top+menumargin, 260, 40), "Levels", style)) {
			Application.LoadLevel("LevelsScene");
		}

		if(GUI.Button(new Rect(left, top+menumargin*2, 260, 40), "Main menu", style)) {
			Application.LoadLevel("SplashScene");
		}		
	}
	
	public bool menuIsActive() {
		return isActive;
	}
}
