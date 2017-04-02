using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIGameOverManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    // removed for optimization, not called
    /*
	void Update () {
	
	}*/

    public void ReturnToSceneMenu(){
		GameStateManager.setGameState (GameState.Menu);
		SceneManager.LoadSceneAsync("MenuScene");
	}
	
	public void ReturnToLevelScene(){
		GameStateManager.setGameState (GameState.Playing);
        SceneManager.LoadSceneAsync("LevelScene");
	}
}
