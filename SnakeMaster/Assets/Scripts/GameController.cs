using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	private Font font;
	
	private static string levelPrefix  = "Level";
	private static string starsPrefix  = "stars_level_";
	
	private static int maxLevel = 5;
	private static int[] unlockStars = {0, 1, 2, 3, 5}; // Min Stars to Unlock Level
	private static int[] levelStars = {1, 1, 1, 2, 3}; // Number of stars on each level
	
	public static bool HasEnoughStarts(int level){
		int stars = 0;
		for(int i = 0; i < level; i++) {
			stars += PlayerPrefs.GetInt("stars_level_" + (i));	
		}
		return stars >= unlockStars[level-1];
	}
	
	public static int numLevels(){
		return maxLevel;
	}
	
	public static int totalLevelStars(int level) {
		return unlockStars[level-1];
	}
	
	public static int inMapLevelStars(int level) {
		return levelStars[level-1];		
	}
	
	public static int inMapPickedLevelStars(int level) {
		return PlayerPrefs.GetInt("stars_level_" + (level));		
	}
	
	public static int leftLevelStars(int level) {
		int stars = 0;
		for(int i = 0; i < level; i++) {
			stars += PlayerPrefs.GetInt("stars_level_" + (i));
		}
		return stars;
	}
	
	public static void ResetPlayerStars(){
		for(int i = 1; i <= maxLevel; i++){
			PlayerPrefs.SetInt(starsPrefix + i, 0);
		}
	}

	public  int currentLevel = 1;
	private int starsCollected = 0;
	
	public void LoadNextLevel(){
		SaveStarts();
		
		int nextLevel = currentLevel + 1;
		string level = CanLoadLevel(nextLevel) ? levelPrefix + nextLevel : "LevelsScene";
		
		Application.LoadLevel(level);
	}
	
	private void SaveStarts(){
		string key = "stars_level_" + currentLevel;
		int maxStars = PlayerPrefs.GetInt(key);
		PlayerPrefs.SetInt(key, Mathf.Max(starsCollected, maxStars));
	}
	
	bool CanLoadLevel(int nextLevel){
		return nextLevel <= maxLevel && HasEnoughStarts(nextLevel);
	}
		
	public void ResetLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void CollectStar(){
		starsCollected++;
	}

	void OnGUI(){
		var style = new GUIStyle(GUI.skin.label);
		style.font = font;
		style.fontSize = 30;
		GUI.Label(new Rect(20, 20, 240, 300), "Level "+currentLevel, style);
	}
	
	void Start(){
		font = Resources.Load("african") as Font;
	}	
}
