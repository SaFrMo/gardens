using UnityEngine;
using System.Collections;

public class AllMiscUnlockables : MonoBehaviour {


	public static MiscUnlockable chicago1Debt = new MiscUnlockable("Debt to the City of Chicago", "You owe the City of Chicago for helping you " +
	                                                               "set up your first garden.", 250, new MiscUnlockable.UnlockAction (chicago1_debt_paid) );
	// add the new 
	private static void chicago1_debt_paid () {
		// remove the debt from the first level
		Catalog.miscUnlockables.Remove (AllMiscUnlockables.chicago1Debt);
		// add the new contract
		Catalog.contractsList.Add (AllContracts.chicago2);
	}
}
