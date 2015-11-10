using UnityEngine;
using System.Collections;

abstract public class parralaxAssetGenerator : MonoBehaviour {

	abstract public void clear ();
	
	abstract public GameObject generateGameObjectAtPosition();
}
