using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllContracts {

	public static Contract chicago1 = new Contract ("Chicago", null, "chicago1", 
	                                                "A government position managing the experimental gardens on small apartment buildings.",
	                                                Contract.City.Chicago, true);
	public static Contract chicago2 = new Contract ("Chicago", null, "chicago1", 
	                                                "FIRE",
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
