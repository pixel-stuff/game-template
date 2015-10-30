using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class parallaxPlan : MonoBehaviour {

	public List<GameObject> visibleGameObjectTab;

	public float distance;

	public GameObject popLimitation;
	public GameObject depopLimitation;
	public float hightSpaceBetweenAsset = 0;
	public float lowSpaceBetweenAsset = 0;

	public assetGenerator generator;

	private float initSpeed = 0.1f;
	private bool isInit = false;

	private float actualSpeed = 0.0f;

	private float spaceBetweenAsset = 0.0f;
	private float speedMultiplicator;

	// Use this for initialization
	void Start () {
		if (distance < 0) {
			speedMultiplicator = 1 - (1 / (1 -distance));
		} else {
			speedMultiplicator = 1 - (1 / (1 + distance));
		}
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
		for (int i=0; i<visibleGameObjectTab.Count; i++) {
			GameObject parrallaxAsset = visibleGameObjectTab[i];
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
		if(spaceBetweenLastAndPopLimitation() > spaceBetweenAsset){
			GameObject asset = generator.generateGameObjectAtPosition(new Vector3(popLimitation.transform.position.x,popLimitation.transform.position.y,this.transform.position.z));
			asset.transform.parent = this.transform;
			visibleGameObjectTab.Add(asset);

			generateNewSpaceBetweenAssetValue();
		}
	}


	void generateNewSpaceBetweenAssetValue(){
		spaceBetweenAsset = Random.Range (lowSpaceBetweenAsset,hightSpaceBetweenAsset);
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
		if (popLimitation.transform.position.x < depopLimitation.transform.position.x) {
			return (parallaxObject.transform.position.x + (parallaxObject.transform.lossyScale.x / 2) < depopLimitation.transform.position.x);// probl"me ici
		} else {
			return (parallaxObject.transform.position.x + (parallaxObject.transform.lossyScale.x / 2) > depopLimitation.transform.position.x);// probl"me ici
		}
		
	}


	float spaceBetweenLastAndPopLimitation() {
		if (visibleGameObjectTab.Count != 0) {
			float space = visibleGameObjectTab[visibleGameObjectTab.Count - 1].transform.position.x - popLimitation.transform.position.x;
			return Mathf.Abs (space);

		} else {
			return float.MaxValue;
		}
	}
}