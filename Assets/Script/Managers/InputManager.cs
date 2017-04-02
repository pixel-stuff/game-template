using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InputManager : MonoBehaviour {

	#region Singleton
	private static InputManager m_instance;
	void Awake(){
		if(m_instance == null){
			//If I am the first instance, make me the Singleton
			m_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}else{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != m_instance)
				Destroy(this.gameObject);
		}
	}
	#endregion Singleton

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		switch (GameStateManager.getGameState ()) {
		case GameState.Menu:
			UpdateMenuState();
			break;
            case GameState.Pause:
            case GameState.Playing:
			UpdatePlayingState();
			break;
            //case GameState.Pause:
            // Do nothing
			//UpdatePauseState();
			break;
		case GameState.GameOver:
			UpdateGameOverState();
			break;
		}
	}

	void UpdateMenuState(){
		if(Input.GetKeyDown(KeyCode.Return)){
			GameStateManager.setGameState (GameState.Playing);
            SceneManager.LoadSceneAsync("LevelScene");
		}
	}

	void UpdatePlayingState(){
        // switcher pause/playing state
		if(Input.GetKeyDown(KeyCode.P)){
            switch(GameStateManager.getGameState())
            {
                case GameState.Playing:
                    GameStateManager.setGameState(GameState.Pause);
                break;
                case GameState.Pause:
                default:
                GameStateManager.setGameState(GameState.Playing);
                break;
            }
		}

        // Only update the controls when playing
        if (GameStateManager.getGameState() == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
            {
                PlayerManager.UP();
            }

            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A))
            {
                PlayerManager.LEFT();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                PlayerManager.DOWN();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                PlayerManager.RIGHT();
            }
        }
	}

	void UpdatePauseState(){
		if(Input.GetKeyDown(KeyCode.P)){
			GameStateManager.setGameState(GameState.Playing);
		}
	}

	void UpdateGameOverState(){

	}

}
