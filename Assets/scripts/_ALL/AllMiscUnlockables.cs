using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllMiscUnlockables : MonoBehaviour {

	public delegate void UnlockAction();
	// REFERENCE to unlocking delegate actions.
	// Since delegates can't be serialized, MiscUnlockable.Unlock() instead searches for this dictionary
	// and runs the relevant delegate

	// TODO: serialization overhaul - fix this. this is gross.
	public static Dictionary<string, UnlockAction> ACTION_LOOKUP = new Dictionary<string, UnlockAction>() {
		{ "Debt to the City of Chicago", new UnlockAction(chicago1_debt_paid ) }
	};

	public static void RunAction (string methodName)
	{
		ACTION_LOOKUP[methodName]();
	}


	public static MiscUnlockable chicago1Debt = new MiscUnlockable("Debt to the City of Chicago", "You owe the City of Chicago for helping you " +
	                                                               "set up your first garden.", 250);
	// add the new 
	private static void chicago1_debt_paid () {
		// remove the debt from the first level
		Catalog.miscUnlockables.Remove (AllMiscUnlockables.chicago1Debt);
		// add the new contract
		Catalog.contractsList.Add (AllContracts.chicago2);
	}
}
