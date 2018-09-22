using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTest : MonoBehaviour {

	public Texture2D cursorImage;
	public Texture2D cursorImageExit;
	private void OnMouseEnter() {
		Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
	}

	private void OnMouseExit() {
		Cursor.SetCursor(cursorImageExit, Vector2.zero, CursorMode.ForceSoftware);	
	}
	
}
