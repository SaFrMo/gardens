using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllContracts {

	public static Contract chicago1 = new Contract ("Chicago: Proof of Concept", null, "chicago1", 
	                                                "A government position managing the experimental gardens on small apartment buildings.",
	                                                Contract.City.Chicago, true);
	public static Contract chicago2 = new Contract ("Chicago: Fire Station", null, "chicago1", 
	                                                "Work for the CFD on a very visible building front. Raise money for them, catch public attention for your work.",
	                                                Contract.City.Chicago, false);











	public static List<Contract> contractsStart = new List<Contract>()
	{
		chicago1
	};

	public static void ContractsReset ()
	{
		Catalog.contractsList = contractsStart;
	}
}
