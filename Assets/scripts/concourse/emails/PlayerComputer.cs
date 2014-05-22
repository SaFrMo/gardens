using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerComputer : MonoBehaviour {

	private List<Email> _inbox = null;
	private Vector2 inboxScrollPos = Vector2.zero;
	private Vector2 emailScrollPos = Vector2.zero;

	private Email currentEmail = null;

	private void ComputerScreen ()
	{

		// TODO: fix layout
		// email window
		float columnWidth = Screen.width * .2f;
		GUILayout.BeginArea (new Rect (Screen.width / 2, Screen.height * .25f, columnWidth, 400f));
		// email logo
		GUILayout.Box (".INBOX.");
		inboxScrollPos = GUILayout.BeginScrollView (inboxScrollPos);
		// select an email in your _inbox
		if (_inbox.Count > 0)
		{
			foreach (Email e in _inbox)
			{
				// mark if email is unread
				string subjectDisplay = (e.Read ? string.Empty : "[new!] ");
				// select email in question
				if (GUILayout.Button (subjectDisplay + e.Subject)) { currentEmail = e; }
			}
		}
		else { GUILayout.Box ("No messages in inbox!"); }
		GUILayout.EndScrollView();
		GUILayout.EndArea();

		// display selected email
		if (currentEmail != null)
		{
			// has the message been read?
			if (!currentEmail.Read) { currentEmail.Read = true; }

			// display content and options
			GUILayout.BeginArea (new Rect (Screen.width / 2 + columnWidth + GameManager.SPACER, Screen.height * .25f, columnWidth, 400f));
			// current email options
			GUILayout.BeginHorizontal();
			// delete email
			// TODO: move to Trash list
			if (GUILayout.Button ("Delete"))
			{
				Email toDelete = _inbox.Find (x => x.Content == currentEmail.Content);
				_inbox.Remove (toDelete);
				GameManager.GAME_MANAGER.GetComponent<PlayerInventory>().trash.Add (toDelete);
				currentEmail = null;
			}
			// mark unread and go back to _inbox
			if (GUILayout.Button ("Mark unread"))
			{
				currentEmail.Read = false;
				currentEmail = null;
			}
			// just go back to inbox
			if (GUILayout.Button ("Back to inbox"))
			{
				currentEmail = null;
			}
			
			GUILayout.EndHorizontal();
			// current email content
			GUI.skin.scrollView.fixedWidth = columnWidth;
			GUI.skin.button.wordWrap = true;
			emailScrollPos = GUILayout.BeginScrollView (emailScrollPos);
			GUILayout.Box (currentEmail.Content);
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}
	}

	private void FindInbox ()
	{
		_inbox = GameManager.GAME_MANAGER.GetComponent<PlayerInventory>().inbox;
	}

	private void Start ()
	{
		Invoke ("FindInbox", .1f);
	}

	private void OnGUI ()
	{
		GUI.skin = GameManager.GUI_SKIN;

		if (_inbox != null)
			ComputerScreen();
	}

}
