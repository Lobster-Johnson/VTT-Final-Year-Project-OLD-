using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

	//networking test code
	void Start () {
	    if(isLocalPlayer)
        {
            
            GetComponent<Creature>().InUse = true;
        }
	}
	
}
