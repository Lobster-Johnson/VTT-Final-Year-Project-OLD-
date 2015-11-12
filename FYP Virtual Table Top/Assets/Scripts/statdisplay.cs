using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class statdisplay : MonoBehaviour {
    public Text displayText;

    //delete this
    string creaturename = "Knight";
    int current_health = 10;
    int max_health = 15;
    int armor = 4;
    int strength = 4;
    int dexterity = 2;
    int speed = 6;
    string status = "feeling fine";
    //end of mockup stats


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        displayText.text = (
            creaturename 
            + "\nHealth: " + current_health + "/" + max_health
            + "\nArmor: " + armor
            + "\nStrength: " + strength
            + "\nDexterity: " + dexterity
            + "\nSpeed: " + speed
            + "\nYou are " + status);
    }
}
