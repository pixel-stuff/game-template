using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class assetGenerator : MonoBehaviour {

	public GameObject prefab;

	public List<GameObject> GameObjectTabOfTypePrefab = new List<GameObject>();

	// Use this for initialization
	public void clear(){
		GameObjectTabOfTypePrefab.Clear ();
	}

	public GameObject generateGameObjectAtPosition() {
		GameObject asset = availableGameobject (GameObjectTabOfTypePrefab);
		if (asset == null){
			asset = Instantiate (prefab);
			GameObjectTabOfTypePrefab.Add (asset);
		}
		return asset; 
	}

	private GameObject availableGameobject(List<GameObject> list){
		foreach(GameObject gameobject in list){
			if (!gameobject.activeSelf){
				gameobject.SetActive(true);
				return gameobject;
			}
		}
		return null;
	}
}
