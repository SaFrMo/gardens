using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckWater : MonoBehaviour {

	private enum DisplayType
	{
		None,
		Overall,
		Location
	}

	private DisplayType infoDisplay = DisplayType.Overall;
	public KeyCode checkKey = KeyCode.R;


	private Vector2 scrollPos;
	private Vector2 scrollPosL;
	private Vector2 scrollPosR;

	private void InfoCell (GrowingPlant plant)
	{
		// TODO: graphical representation
		GUILayout.Box (string.Format ("{0}\n{1}\nWorth ${2}", plant.name, (plant.CurrentWaterLevel * 100).ToString() + "%", plant.CurrentSellingPrice));
	}

	private List<GrowingPlant> left = new List<GrowingPlant>();
	private List<GrowingPlant> right = new List<GrowingPlant>();
	private List<GrowingPlant> middle = new List<GrowingPlant>();


	private void ShowPlantInfo () 
	{
		switch (infoDisplay)
		{

		case DisplayType.None:
			break;

		case DisplayType.Overall:
			float boxHeight = Screen.height / 2;
			GUILayout.BeginArea (new Rect (GameManager.SPACER,
			                               Screen.height / 2 - boxHeight / 2,
			                               100f,
			                               boxHeight));
			scrollPos = GUILayout.BeginScrollView(scrollPos, GUIStyle.none);
			foreach (GrowingPlant g in Planter.ALL_PLANTS)
			{
				if (g != null)
					InfoCell (g);
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
			break;

		case DisplayType.Location:
			// TODO: take into account vertical location
			boxHeight = Screen.height / 2;
			foreach (GrowingPlant g in Planter.ALL_PLANTS)
			{
				if (g != null)
				{
					if (Camera.main.WorldToViewportPoint (g.transform.position).x < 0)
					{
						if (!left.Contains (g))
							left.Add (g);
						if (middle.Contains (g))
							middle.Remove (g);
						if (right.Contains (g))
							right.Remove (g);
					}
					else if (Camera.main.WorldToViewportPoint (g.transform.position).x > 1)
					{
						if (!right.Contains (g))
							right.Add (g);
						if (left.Contains (g))
							left.Remove (g);
						if (middle.Contains (g))
							middle.Remove (g);
					}
					else
					{
						if (!middle.Contains (g))
							middle.Add (g);
						if (left.Contains (g))
							left.Remove (g);
						if (right.Contains (g))
							right.Remove (g);
					}
				}
			}

			GUILayout.BeginArea (new Rect (GameManager.SPACER,
			                               Screen.height / 2 - boxHeight / 2,
			                               100f,
			                               boxHeight));
			scrollPosL = GUILayout.BeginScrollView(scrollPosL, GUIStyle.none);
			foreach (GrowingPlant g in left)
			{
				if (g != null)
					InfoCell (g);
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();

			GUILayout.BeginArea (new Rect (Screen.width - 100f - GameManager.SPACER,
			                               Screen.height / 2 - boxHeight / 2,
			                               100f,
			                               boxHeight));
			scrollPosR = GUILayout.BeginScrollView(scrollPosR, GUIStyle.none);
			foreach (GrowingPlant g in right)
			{
				if (g != null)
					InfoCell (g);
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();

			foreach (GrowingPlant g in middle)
			{
				if (g != null)
				{
					GUI.Box (SaFrMo.GUIOverObject (g.gameObject), 
					         string.Format ("{0}\n{1}\nWorth ${2}", g.name, (g.CurrentWaterLevel * 100).ToString() + "%", g.CurrentSellingPrice));
				}
			}

			break;




		};
	}





	private void OnGUI()
	{
		ShowPlantInfo();
	}

	private void Update ()
	{
		if (Input.GetKeyDown (checkKey))
		{
			switch (infoDisplay)
			{
			case DisplayType.None:
				infoDisplay = DisplayType.Overall;
				break;
			case DisplayType.Overall:
				infoDisplay = DisplayType.Location;
				break;
			case DisplayType.Location:
				infoDisplay = DisplayType.None;
				break;
			};
		}
	}
}
