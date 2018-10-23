using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {

	public List<Texture2D> cursorImage;
	private static CursorManager instance;
	public static CursorManager getInstance()
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
	}
	void Start() {
		SetCursor(CursorConstant.NORMAL);	
	}

	public void SetCursor(int index)
	{
		Cursor.SetCursor(cursorImage[index], Vector2.zero, CursorMode.ForceSoftware);
	} 

	public class CursorConstant
	{
		public const int NORMAL = 0;
		public const int EXPAND = 1;
		public const int ROTATION = 2;
	}
}
