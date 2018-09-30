using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;


public class LocalizationManager : MonoBehaviour {

	public enum LanguageKind { KOREAN, ENGLISH }
	public LanguageKind TargetLanguage = LanguageKind.KOREAN;
	
	public Dictionary<string, string> textSet { get; private set;}

	void Awake() {
		LoadJson();
	}
	private void LoadJson()
	{
		string targetPath = "";
		switch(TargetLanguage)
		{
			case LanguageKind.KOREAN:
			targetPath = "ko.json";
				break;

			case LanguageKind.ENGLISH:
			targetPath = "en.json";
				break;
		}
		string path = "/Users/chan/BlueSemester/Assets/Resources/Language";
		//Application.persistentDataPath+"//Assets//Resources//Language//"
		StreamReader sr = new StreamReader(File.Open(path + "/" + targetPath, FileMode.Open));
		using (sr)
		{
			string file = sr.ReadToEnd();
			textSet = JsonUtility.FromJson<Dictionary<string,string>>(file);

			//Debug
			foreach (var s in textSet.Values)
			{
				Debug.Log(s);
			}
		}
	}
	
}
