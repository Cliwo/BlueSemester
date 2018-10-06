using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;


public class LocalizationManager : MonoBehaviour {

	public enum LanguageKind { KOREAN, ENGLISH }
	public LanguageKind TargetLanguage = LanguageKind.KOREAN;
	
	public Dictionary<string, string> textSet { get; private set;}
	private static LocalizationManager instance;
	public static LocalizationManager getInstance()
	{
		return instance;
	}

	void Awake() {
		if(instance == null)
		{
			instance = this;
		}
		if(instance != this)
		{
			DestroyImmediate(this);
		}
		LoadJson();
	}

	public string GetText(string key)
	{
		string text;
		textSet.TryGetValue(key, out text);
		return text;
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
		StreamReader sr = new StreamReader(File.Open(path + "/" + targetPath, FileMode.Open));
		using (sr)
		{
			string file = sr.ReadToEnd();
			textSet = JsonUtility.FromJson<Dictionary<string,string>>(file);

			//Debug
			textSet = new Dictionary<string, string>();
			textSet.Add("dummy00", "Hi");
			textSet.Add("dummy01", "How are you?");
		}
	}
	

}
