using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UILevelManager : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}




    // Update is called once per frame
    // removed for optimization, not called
    /*void Update () {
	
	}*/


	public void ReturnToSceneMenu(){
		GameStateManager.setGameState (GameState.Menu);
        SceneManager.LoadSceneAsync("MenuScene");
    }

	public void GoToGameOverScene(){
		GameStateManager.setGameState (GameState.GameOver);
        SceneManager.LoadSceneAsync("GameOverScene");

	}
}
