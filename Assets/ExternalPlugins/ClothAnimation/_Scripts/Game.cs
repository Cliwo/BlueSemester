using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	public static Game Instance;

	public bool Death = false;

	// Use this for initialization
	void Awake()
	{
		Instance = this;
		//Death = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
