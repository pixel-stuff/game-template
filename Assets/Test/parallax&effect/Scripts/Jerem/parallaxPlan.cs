using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class parallaxPlan : MonoBehaviour {
	public float distance;
	
	public GameObject popLimitation;
	public GameObject depopLimitation;
	public float hightSpaceBetweenAsset = 0;
	public float lowSpaceBetweenAsset = 0;


	public parralaxAssetGenerator generator;

	abstract public void setSpeedOfPlan(float newSpeed);
			

}