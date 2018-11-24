using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
public class TestSerialize : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		DebugDeserialize();
	}
	
	private void DebugSerialize()
	{
		GameStateModel model = new GameStateModel();

		GameStateModel.SerializableVector3 vec = new GameStateModel.SerializableVector3();
		vec.x = 10.0f;
		vec.y = 20.0f;
		vec.z = 5.0f;

		Dictionary<string, int> test = new Dictionary<string, int>();
		test.Add("Hi", 3);
		test.Add("Hello", 2);

		model.lastPosition = vec;
		model.itemList = test;
		model.currentHP = 70.0f;
		model.FireDungeonCleared = false;
		GameStateModel.FlushToFile(GameStateModel.Serialize(model));
	}
	private void DebugDeserialize()
	{
		string path = GameStateModel.GetSaveLocation();
		byte[] data = File.ReadAllBytes(path);
		GameStateModel model = GameStateModel.Deserialize(data);
		Debug.Log(model);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
