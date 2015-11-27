using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ParralaxPlanConfiguration : System.Object
{
	public GameObject prefabParralaxPlan;
	public float distance;
	public parralaxAssetGenerator generatorScript;
	public float lowSpaceBetweenAsset;
	public float hightSpaceBetweenAsset;
}

public class parralaxManager : MonoBehaviour {
	public ParralaxPlanConfiguration[] configurationParralax;
	public GameObject rightBorder;
	public GameObject leftBorder;
	public List<GameObject> parralaxPlans;

	public Camera cameraToFollow = null;

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

		float zinf = 2.0f;
		float zsupp = -2.0f;
		foreach (GameObject temp in parralaxPlans) {
			if(temp.GetComponent<parallaxPlan>().distance < 0){
				temp.transform.localPosition = new Vector3(temp.transform.localPosition.x,temp.transform.localPosition.y,temp.transform.localPosition.z + zinf++);
			} else if(temp.GetComponent<parallaxPlan>().distance == 0) {
				temp.transform.localPosition = new Vector3(temp.transform.localPosition.x,temp.transform.localPosition.y,temp.transform.localPosition.z );
			} else {
				temp.transform.localPosition = new Vector3(temp.transform.localPosition.x,temp.transform.localPosition.y,temp.transform.localPosition.z+ zsupp--);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//reset the Pop and depop position 
		float cameraSize = cameraToFollow.orthographicSize*2;
		float CameraW = cameraToFollow.rect.width;
		rightBorder.transform .position = new Vector3 (CameraW * cameraSize,rightBorder.transform .position.y,rightBorder.transform .position.z);
		leftBorder.transform .position = new Vector3 (-CameraW * cameraSize,leftBorder.transform .position.y,leftBorder.transform .position.z);
			float cameraSpeedX=0;
			if (cameraToFollow != null){
				cameraSpeedX = (cameraToFollow.transform.position.x - this.transform.position.x)*10;
				this.transform.position = new Vector3(cameraToFollow.transform.position.x, this.transform.position.y, this.transform.position.z);
			}
			Debug.Log("speed : " + cameraSpeedX);
		foreach (GameObject plan in parralaxPlans) {
			plan.GetComponent<parallaxPlan> ().setSpeedOfPlan (speed+ cameraSpeedX);
		}
	}
}
