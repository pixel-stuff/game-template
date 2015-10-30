using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ParralaxPlanConfiguration : System.Object
{
	public GameObject prefabParralaxPlan;
	public float distance;
	public assetGenerator generatorScript;
	public float lowSpaceBetweenAsset;
	public float hightSpaceBetweenAsset;
}

public class parralaxManager : MonoBehaviour {
	public ParralaxPlanConfiguration[] configurationParralax;
	public GameObject rightBorder;
	public GameObject leftBorder;
	public List<GameObject> parralaxPlans;

	public float speed;
	// Use this for initialization
	void Start () {
		parralaxPlans.Clear ();
		foreach (ParralaxPlanConfiguration config in configurationParralax) {
			GameObject tempParralaxPlan = Instantiate(config.prefabParralaxPlan);
			tempParralaxPlan.transform.parent = this.transform;

			parallaxPlan tempScript = tempParralaxPlan.GetComponent<parallaxPlan>();
			tempScript.popLimitation = rightBorder;
			tempScript.depopLimitation = leftBorder;
			tempScript.generator = config.generatorScript;
			tempScript.distance = config.distance;
			tempScript.lowSpaceBetweenAsset = config.lowSpaceBetweenAsset;
			tempScript.hightSpaceBetweenAsset = config.hightSpaceBetweenAsset;

			parralaxPlans.Add(tempParralaxPlan);
		}
		parralaxPlans.Sort(delegate(GameObject x, GameObject y)
		{
			parallaxPlan tempScriptX = x.GetComponent<parallaxPlan>();
			parallaxPlan tempScriptY = y.GetComponent<parallaxPlan>();
			if (tempScriptX.distance == tempScriptY.distance) {
				return 0;
			} else if (tempScriptX.distance < tempScriptY.distance) {
				return 1;
			} else return -1;
		});

		float zinf = -1.0f;
		float zsupp = 1.0f;
		foreach (GameObject temp in parralaxPlans) {
			if(temp.GetComponent<parallaxPlan>().distance > 0){
				temp.transform.localPosition = new Vector3(temp.transform.localPosition.x,temp.transform.localPosition.y,temp.transform.localPosition.z + zinf--);
			} else {
				temp.transform.localPosition = new Vector3(temp.transform.localPosition.x,temp.transform.localPosition.y,temp.transform.localPosition.z + zsupp++);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject plan in parralaxPlans) {
			plan.GetComponent<parallaxPlan> ().setSpeedOfPlan (speed);
		}
	}
}
