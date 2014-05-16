using UnityEngine;
using System.Collections;

public class Tooltips {

	// Tooltip system
	public static void ToolTipBox (string message) {
		GUI.Box (new Rect (Event.current.mousePosition.x, Event.current.mousePosition.y, 300f, 100f), message);
	}
	
	private static void WindowFun (int id) {}
	public static void ShowToolTip (string message) {
		GUI.Window (0, new Rect (Input.mousePosition.x + 10f, SaFrMo.InputYToGUIY(Input.mousePosition.y), 300f, 150f), WindowFun, message);
		GUI.BringWindowToFront(0);
	}
	// standalone - place anywhere
	public static void ShowToolTip (string message, Rect reference) {
		if (reference.Contains(Event.current.mousePosition)) {
			ShowToolTip (message);
		}
	}
}
