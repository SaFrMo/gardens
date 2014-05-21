using UnityEngine;
using System.Collections;

public class Email {

	public string To { get; private set; }
	public string From { get; private set; }
	public string Content { get; private set; }
	public string Subject { get; private set; }
	public bool Read { get; set; }

	public Email (string to, string from, string content, string subject = "(no subject)")
	{
		To = to;
		From = from;
		Content = content;
		Subject = subject;
		Read = false;
	}
}
