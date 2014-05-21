using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerComputer : MonoBehaviour {

	private List<Email> inbox = new List<Email>()
	{
		new Email("you", "Dan", "this is a test")
	};


	private Email currentEmail = null;

	private void ComputerScreen ()
	{
		// TODO: fix layout
		float columnWidth = Screen.width * .2f;
		GUILayout.BeginArea (new Rect (Screen.width / 2, Screen.height * .25f, columnWidth, 400f));
		// select an email in your inbox
		if (inbox.Count > 0)
		{
			foreach (Email e in inbox)
			{
				// mark if email is unread
				string subjectDisplay = (e.Read ? string.Empty : "[new!] ");
				// select email in question
				if (GUILayout.Button (subjectDisplay + e.Subject)) { currentEmail = e; }
			}
		}
		else { GUILayout.Box ("No messages in inbox!"); }
		GUILayout.EndArea();

		// display selected email
		if (currentEmail != null)
		{
			// has the message been read?
			if (!currentEmail.Read) { currentEmail.Read = true; }

			// display content and options
			GUILayout.BeginArea (new Rect (Screen.width / 2 + columnWidth + GameManager.SPACER, Screen.height * .25f, columnWidth, 400f));
			GUILayout.Box (currentEmail.Content);

			// delete email
			// TODO: move to Trash list
			GUILayout.BeginHorizontal();
			if (GUILayout.Button ("Delete"))
			{
				inbox.Remove (inbox.Find (x => x.Content == currentEmail.Content));
				currentEmail = null;
			}
			// mark unread and go back to inbox
			if (GUILayout.Button ("Mark unread"))
			{
				currentEmail.Read = false;
				currentEmail = null;
			}
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		} 
	}

	private void OnGUI ()
	{
		ComputerScreen();
	}

}
