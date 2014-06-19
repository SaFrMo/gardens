using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SaFrMo {
	
	static System.Random rand = new System.Random();

	/// <summary>
	/// Checks if a Vector3 is close enough to a target position.
	/// </summary>
	/// <returns><c>true</c>, if enough was closed, <c>false</c> otherwise.</returns>
	/// <param name="current">Current.</param>
	/// <param name="target">Target.</param>
	/// <param name="minDistance">Minimum distance.</param>
	public static bool CloseEnough (Vector3 current, Vector3 target, float minDistance = .3f) {
		return Vector3.Distance (current, target) <= minDistance;
	}

	/// <summary>
	/// Checks if a Vector2 is close enough to a target position.
	/// </summary>
	/// <returns><c>true</c>, if enough was closed, <c>false</c> otherwise.</returns>
	/// <param name="current">Current.</param>
	/// <param name="target">Target.</param>
	/// <param name="minDistance">Minimum distance.</param>
	public static bool CloseEnough (Vector2 current, Vector2 target, float minDistance = .3f) {
		return Vector2.Distance (current, target) <= minDistance;
	}
	
	
	/// <summary>
	/// Move the specified GameObject in the specified direction at the specified rate. Requires a Rigidbody attached to the GameObject in question.
	/// </summary>
	/// <param name="rate">Rate.</param>
	/// <param name="direction">Direction.</param>
	/// <param name="go">Go.</param>
	public static void MoveObject (float rate, Vector3 direction, GameObject go) {
		go.rigidbody.MovePosition (go.transform.position + 
		                           direction * rate * Time.deltaTime);
	}
	
	/// <summary>
	/// Gets a random true/false value (boolean value).
	/// </summary>
	/// <returns><c>true</c>, if random bool was gotten, <c>false</c> otherwise.</returns>
	public static bool GetRandomBool() {
		return (rand.NextDouble() > 0.5);
	}
	
	
	/// <summary>
	/// Rotates and resizes the object. Useful for attention-grabbing things - powerups, etc. Call on the object's Update().
	/// </summary>
	/// To make this more flexible, could add Vector3 v to the signature, but seems unnecessary for now
	/// The 2f moves the sin function up the Y axis so the value doesn't come back as 0 or negative
	public static void RotateAndResize (Transform t, float rotationRate, float resizeRate) {
		t.RotateAround (t.position, t.TransformDirection (Vector3.forward), rotationRate * Time.deltaTime);
		t.localScale = Vector3.one * (Mathf.Sin (Time.time * resizeRate) + 2f);
	}
	
	/// <summary>
	/// Creates a solid color box - avoids need to manually create solid-color textures
	/// </summary>
	/// <returns>The color.</returns>
	/// <param name="color">Color.</param>
	public static Texture2D CreateColor (Color color) {
		Texture2D newTexture = new Texture2D (1, 1);
		newTexture.SetPixel (0, 0, color);
		newTexture.Apply();
		return newTexture;
	}
	
	/// <summary>
	/// Returns circle points. Think InfoAddict in Civ V.
	/// </summary>
	/// <param name="points">Points.</param>
	/// <param name="radius">Radius.</param>
	/// <param name="center">Center.</param>
	public static List<Vector3> DrawCirclePoints(int points, float radius, float centerX, float centerY)
	{
		List<Vector3> toReturn = new List<Vector3>();
		float slice = 2 * Mathf.PI / points;
		for (int i = 0; i < points; i++)
		{
			float angle = slice * i;
			float newX = (float)(centerX + radius * Mathf.Cos(angle));
			float newY = (float)(centerY + radius * Mathf.Sin(angle));
			toReturn.Add (new Vector3(newX, newY));
		}
		return toReturn;
	}
	
	/// <summary>
	/// Gets a random string from a list.
	/// </summary>
	/// <returns>The random string from list.</returns>
	/// <param name="source">Source.</param>
	public static string GetRandomStringFromList (List<string> source) {
		string toReturn = source[UnityEngine.Random.Range (0, source.Count - 1)];
		return toReturn;
	}
	
	/// <summary>
	/// Converts screen coordinates to GUI coordinates.
	/// </summary>
	/// <returns>The Y to GUI.</returns>
	/// <param name="currentY">Current y.</param>
	public static float InputYToGUIY (float currentY) {
		return -currentY + Screen.height;
	}
	
	/// <summary>
	/// Returns a Rect that matches an object's rendered position. Best used on Update or FixedUpdate.
	/// </summary>
	/// <returns></returns>
	/// <param name="go">Go.</param>
	public static Rect GUIOverObject (GameObject go) {
		// grab the room's location in space
		// the +1f accounts for a rounding error
		return new Rect (Camera.main.WorldToScreenPoint(go.renderer.bounds.min).x,
		                 Screen.height - Camera.main.WorldToScreenPoint(go.renderer.bounds.max).y, 
		                 Camera.main.WorldToScreenPoint (go.renderer.bounds.max).x - Camera.main.WorldToScreenPoint(go.renderer.bounds.min).x ,
		                 Camera.main.WorldToScreenPoint (go.renderer.bounds.max).y - Camera.main.WorldToScreenPoint(go.renderer.bounds.min).y );
	}
	
	private static int m_frameCounter = 0;
	private static float m_timeCounter = 0.0f;
	private static float m_lastFramerate = 0.0f;
	private static float m_refreshTime = 0.5f;
	/// <summary>
	/// Gets the FPS. Call in Update.
	/// </summary>
	/// <returns>The FP.</returns>
	public static float GetFPS () {
		//Declare these in your class
		if( m_timeCounter < m_refreshTime )
		{
			m_timeCounter += Time.deltaTime;
			m_frameCounter++;
		}
		else
		{
			//This code will break if you set your m_refreshTime to 0, which makes no sense.
			m_lastFramerate = (float)m_frameCounter/m_timeCounter;
			m_frameCounter = 0;
			m_timeCounter = 0.0f;
		}
		
		return m_lastFramerate;
	}

}
