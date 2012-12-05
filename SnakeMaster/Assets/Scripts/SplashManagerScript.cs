using UnityEngine;
using System.Collections;

public class SplashManagerScript : MonoBehaviour {
	
	public Font font;
	
	void Update(){
		if(Input.GetButton("Jump")) Application.LoadLevel(1);
	}
	
	void OnGUI(){
		setTitle();
		setMenu();
	}

	private void setTitle() {
		var style = getTitleStyle(Color.white);
		GUI.Label(new Rect((Screen.width/2)-300, 160, 600, 100), "Snake Master", style);
	}	
	
	private void setMenu() {
		var style = getSubtitleStyle(Color.yellow);
		int left = (Screen.width/2)-170;
		int top = 300;
		int menumargin = 50;

		if(GUI.Button(new Rect(left, top, 340, 40), "Start new game", style)) {
			Application.LoadLevel(1);
		}
		
		if(GUI.Button(new Rect(left, top+menumargin, 340, 40), "Continue last game", style)) {
			Application.LoadLevel(1);
		}
		
//		if(GUI.Button(new Rect(left, top+menumargin*2, 340, 40), "Levels", style)) {
//			Application.LoadLevel(1);
//		}

		if(GUI.Button(new Rect(left, top+menumargin*2, 340, 40), "Quit", style)) {
			Application.Quit();
		}		
	}
	
	private GUIStyle getTitleStyle(Color color){
		var style = new GUIStyle();
		style.font = font;
		style.fontSize = 55;
		style.normal.textColor = color;
		style.alignment = TextAnchor.UpperCenter;
		return style;
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

}
