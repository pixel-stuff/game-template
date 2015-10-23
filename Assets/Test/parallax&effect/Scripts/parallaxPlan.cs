using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class parallaxPlan : MonoBehaviour {

	public List<GameObject> visibleGameObjectTab;

	public float speedMultiplicator;

	public GameObject popLimitation;
	public GameObject depopLimitation;

	public assetGenerator generator;

	private float initSpeed = 0.1f;
	private bool isInit = false;

	private float actualSpeed =0.0f;

	// Use this for initialization
	void Start () {
		generator.clear ();
		while (!isInit) {
			moveAsset (initSpeed);
			generateAssetIfNeeded ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		moveAsset (actualSpeed * speedMultiplicator);
		generateAssetIfNeeded ();
}

	void moveAsset(float speed){
		foreach(GameObject parrallaxAsset in visibleGameObjectTab){
			Vector3 positionAsset = parrallaxAsset.transform.position;
			if (!isStillVisible(parrallaxAsset)){
				parrallaxAsset.SetActive(false);
				visibleGameObjectTab.Remove(parrallaxAsset);
				isInit =true;
			} else {
				positionAsset.x -= speed;
				parrallaxAsset.transform.position = positionAsset;
			}
		}
	}


	void generateAssetIfNeeded(){
		if(spaceBetweenLastAndPopLimitation() > 5.0f){
			//generator.generateGameObjectAtPosition(popLimitation.transform.position);
			visibleGameObjectTab.Add(generator.generateGameObjectAtPosition(popLimitation.transform.position));
			/*foreach(GameObject parrallaxAsset in visibleGameObjectTab){
				if (!parrallaxAsset.active){
					parrallaxAsset.SetActive(true);
					parrallaxAsset.transform.position = popLimitation.transform.position;
				}
			}*/
		}
	}
	public void setSpeedOfPlan(float newSpeed){
		if (actualSpeed * newSpeed < 0) {
			swapPopAndDepop();
			print("Swap");
		}
		actualSpeed = newSpeed;
	}

	void swapPopAndDepop(){
		GameObject temp = popLimitation;
		popLimitation = depopLimitation;
		depopLimitation = temp;
	}


	bool isStillVisible (GameObject parallaxObject) {
		return (parallaxObject.transform.position.x + (parallaxObject.transform.lossyScale.x / 2) > depopLimitation.transform.position.x);// probl"me ici
		
	}


	float spaceBetweenLastAndPopLimitation() {
		if (visibleGameObjectTab.Count != 0) {
			float space = visibleGameObjectTab[visibleGameObjectTab.Count - 1].transform.position.x - popLimitation.transform.position.x;
			//print(Mathf.Abs (space));
			return Mathf.Abs (space);

		} else {
			return float.MaxValue;
		}
	}
}