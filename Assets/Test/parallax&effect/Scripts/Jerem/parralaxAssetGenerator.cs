using UnityEngine;
using System.Collections;

abstract class ShapesClass
{
	abstract public int Area();
}

abstract public class parralaxAssetGenerator : MonoBehaviour {

	abstract public void clear ();
	
	abstract public GameObject generateGameObjectAtPosition();
}
