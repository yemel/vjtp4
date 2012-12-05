using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	private static string levelPrefix  = "Level";
	private static string starsPrefix  = "stars_level_";
	
	private static int maxLevel = 4;
	private static int[] unlockStars = {0, 1, 2, 3}; // Min Stars to Unlock Level
	
	public static bool HasEnoughStarts(int level){
		int stars = PlayerPrefs.GetInt("stars_level_" + (level-1));
		return stars >= unlockStars[level-1];
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
}
