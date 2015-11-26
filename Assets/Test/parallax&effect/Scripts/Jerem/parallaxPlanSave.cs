using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class parallaxPlanSave : parallaxPlan {
	
	public List<GameObject> visibleGameObjectTab;
	
	public float space;
	
	private float initSpeed = 0.1f;
	private bool isInit = false;
	
	private float actualSpeed = 0.0f;
	
	private float spaceBetweenAsset = 0.0f;
	private float speedMultiplicator;
	
	private int speedSign = 1;

	public List<StockAssetStruct> m_stockAsset;
	public int hightId = 0;
	public int lowId = 0;

	//public List<int> 

	// Use this for initialization
	void Start () {
		m_stockAsset = new List<StockAssetStruct>();
		actualSpeed = 1;
		if (distance < 0) {
			speedMultiplicator = 1/ -distance;//1 - (1 / (1 -distance));
		} else {
			speedMultiplicator = 1 +  distance/10;//1 - (1 / (1 + distance));
		}
		generator.clear ();
		while (!isInit) {
			moveAsset (initSpeed);
			//			Debug.Log();
			generateAssetIfNeeded ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		moveAsset (actualSpeed * speedMultiplicator);
		generateAssetIfNeeded ();
	}
	
	void moveAsset(float speed){
		List<GameObject> temp = new List<GameObject>();
		foreach(GameObject g in visibleGameObjectTab) {
			if(temp.Contains(g)){
				Debug.Log("WTF§§§§§§!!!!!!");
			}else {
				temp.Add(g);
			}
		}

		for (int i=0; i<temp.Count; i++) {
			GameObject parrallaxAsset = temp[i];
			Vector3 positionAsset = parrallaxAsset.transform.position;
			if (!isStillVisible(parrallaxAsset)){
				if(speedSign >0){
					lowId++;
				}else {
					hightId--;
				}
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
		/*if(((spaceBetweenLastAndPopLimitation() < (-spaceBetweenAsset + actualSpeed * speedMultiplicator)) && (speedSign > 0)) ||
		   ((spaceBetweenLastAndPopLimitation() > (spaceBetweenAsset + actualSpeed * speedMultiplicator)) && (speedSign < 0))){*/
			if(speedSign > 0){
				
			if(hightId == m_stockAsset.Count || hightId == m_stockAsset.Count-1) {
				if(spaceBetweenLastAndPopLimitation() < (-spaceBetweenAsset + actualSpeed * speedMultiplicator)) {
					Debug.Log("generate Hight");
					GenerateAssetStruct assetStruct = generator.generateGameObjectAtPosition();
					GameObject asset = assetStruct.generateAsset;
					asset.transform.parent = this.transform;
					asset.transform.position = new Vector3(popLimitation.transform.position.x + (speedSign * asset.GetComponent<SpriteRenderer> ().sprite.bounds.max.x) + (space-spaceBetweenAsset),popLimitation.transform.position.y,this.transform.position.z);
					visibleGameObjectTab.Add(asset);
					StockAssetStruct stockAssetStruct = new StockAssetStruct();
					stockAssetStruct.code = assetStruct.code;
					stockAssetStruct.dist = spaceBetweenAsset;
					m_stockAsset.Add(stockAssetStruct);
					hightId ++;
					generateNewSpaceBetweenAssetValue();

				}
				} else { // si on a une valeur 
				if(spaceBetweenLastAndPopLimitation() < (-m_stockAsset[hightId +1].dist + actualSpeed * speedMultiplicator)) {
					Debug.Log("get old Hight");
					GenerateAssetStruct assetStruct = generator.generateGameObjectWithCode(m_stockAsset[hightId +1].code);
					GameObject asset = assetStruct.generateAsset;
					asset.transform.parent = this.transform;
					asset.transform.position = new Vector3(popLimitation.transform.position.x + (speedSign * asset.GetComponent<SpriteRenderer> ().sprite.bounds.max.x)+ (space-m_stockAsset[hightId +1].dist),popLimitation.transform.position.y,this.transform.position.z);
					visibleGameObjectTab.Add(asset);
					hightId ++;

				}
				}
			} else { // speed <0
				if (lowId == 0) {
				if(spaceBetweenLastAndPopLimitation() > (spaceBetweenAsset + actualSpeed * speedMultiplicator)) {
					GenerateAssetStruct assetStruct = generator.generateGameObjectAtPosition();
					GameObject asset = assetStruct.generateAsset;
					asset.transform.parent = this.transform;
					asset.transform.position = new Vector3(popLimitation.transform.position.x + (speedSign * asset.GetComponent<SpriteRenderer> ().sprite.bounds.max.x)+ (space-spaceBetweenAsset),popLimitation.transform.position.y,this.transform.position.z);
					visibleGameObjectTab.Add(asset);
					StockAssetStruct stockAssetStruct = new StockAssetStruct();
					stockAssetStruct.code = assetStruct.code;
					stockAssetStruct.dist = spaceBetweenAsset;
					m_stockAsset.Add(stockAssetStruct);
					hightId++;
					generateNewSpaceBetweenAssetValue();
					Debug.Log("generate low");
				}
				} else {
				if(spaceBetweenLastAndPopLimitation() > (m_stockAsset[lowId -1].dist + actualSpeed * speedMultiplicator)){
					GenerateAssetStruct assetStruct = generator.generateGameObjectWithCode(m_stockAsset[lowId -1].code);
					GameObject asset = assetStruct.generateAsset;
					asset.transform.parent = this.transform;
					asset.transform.position = new Vector3(popLimitation.transform.position.x + (speedSign * asset.GetComponent<SpriteRenderer> ().sprite.bounds.max.x)+ (space-m_stockAsset[lowId -1].dist),popLimitation.transform.position.y,this.transform.position.z);
					visibleGameObjectTab.Add(asset);
					lowId--;
					Debug.Log("get old low");
				}
					}
				//}
			}
			/*
			GenerateAssetStruct assetStruct = generator.generateGameObjectAtPosition();
			GameObject asset = assetStruct.generateAsset;
			asset.transform.parent = this.transform;
			asset.transform.position = new Vector3(popLimitation.transform.position.x + (speedSign * asset.GetComponent<SpriteRenderer> ().sprite.bounds.max.x),popLimitation.transform.position.y,this.transform.position.z);
			visibleGameObjectTab.Add(asset);
			generateNewSpaceBetweenAssetValue();
			*/
		//}
	}
	
	
	void generateNewSpaceBetweenAssetValue(){
		spaceBetweenAsset = - Random.Range (lowSpaceBetweenAsset,hightSpaceBetweenAsset) * speedSign;
		/*if (hightId == m_stockAsset.Count) {
			spaceBetweenAsset = Random.Range (lowSpaceBetweenAsset, hightSpaceBetweenAsset);
		} else {
			spaceBetweenAsset = m_stockAsset[hightId +1].dist;
		}*/
	}
	
	
	public override void setSpeedOfPlan(float newSpeed){
		if ((actualSpeed > 0 && speedSign < 0) || (actualSpeed < 0 && speedSign > 0)) {
			swapPopAndDepop ();
			
			print ("Swap");
		}
		actualSpeed = newSpeed;
	}
	
	void swapPopAndDepop(){
		GameObject temp = popLimitation;
		popLimitation = depopLimitation;
		depopLimitation = temp;
		speedSign = speedSign * -1;
	}
	
	
	bool isStillVisible (GameObject parallaxObject) {
		if (speedSign < 0) {
			return (parallaxObject.transform.position.x - (parallaxObject.GetComponent<SpriteRenderer> ().sprite.bounds.max.x ) < depopLimitation.transform.position.x);// probl"me ici
		} else {
			return (parallaxObject.transform.position.x + (parallaxObject.GetComponent<SpriteRenderer> ().sprite.bounds.max.x ) > depopLimitation.transform.position.x);// probl"me ici
		}
	}
	
	
	float spaceBetweenLastAndPopLimitation() {
		if (visibleGameObjectTab.Count != 0) {
			if (speedSign > 0){
				space = getMaxValue();
			}else {
				space = getMinValue();/*Mathf.Min( 
				                 (visibleGameObjectTab[visibleGameObjectTab.Count - 1].transform.position.x -(visibleGameObjectTab[visibleGameObjectTab.Count - 1].GetComponent<SpriteRenderer> ().sprite.bounds.max.x)) - popLimitation.transform.position.x,
				                 (visibleGameObjectTab[0].transform.position.x -(visibleGameObjectTab[0].GetComponent<SpriteRenderer> ().sprite.bounds.max.x)) - popLimitation.transform.position.x
				                 );*/
				//Debug.Log("space speed < 0 : " + space);
			}
			return space;
			
		} else {
			return - float.MaxValue;
		}
	}
	
	
	float getMaxValue(){
		float max = -1000;
		foreach(GameObject g in visibleGameObjectTab){
			float result  = (g.transform.position.x +(visibleGameObjectTab[visibleGameObjectTab.Count - 1].GetComponent<SpriteRenderer> ().sprite.bounds.max.x)) - popLimitation.transform.position.x;
			if (result > max){
				max = result;
			}
		}
		return max;
	}
	
	float getMinValue(){
		float min = 1000;
		foreach(GameObject g in visibleGameObjectTab){
			float result  = (g.transform.position.x -(g.GetComponent<SpriteRenderer> ().sprite.bounds.max.x)) - popLimitation.transform.position.x;
			if (result < min){
				min = result;
			}
		}
		return min;
	}
}