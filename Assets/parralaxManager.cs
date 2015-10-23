using UnityEngine;
using System.Collections;

public class parralaxManager : MonoBehaviour {

	public GameObject[] parralaxPlans;

	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject plan in parralaxPlans) {
			plan.GetComponent<parallaxPlan> ().setSpeedOfPlan (speed);
		}
	}
}
