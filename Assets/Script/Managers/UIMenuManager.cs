using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIMenuManager : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    // removed for optimization, not called
    // code not used
    /*void Update () {
		if (a != null) {
			Debug.Log ("LOADING : " + a.progress);
			Debug.Log ("is done : " + a.isDone + "(" + a.progress*100f +"%)" );
		}
		if (Time.time - timeStartLoading >= 10f) {
			a.allowSceneActivation = true;
		}
    }*/

    AsyncOperation a;
	float timeStartLoading;

	public void GoToLevelScene(){
		GameStateManager.setGameState (GameState.Playing);
        a = SceneManager.LoadSceneAsync("LevelScene");
		//a.allowSceneActivation = false;
		timeStartLoading = Time.time;
	}
}
