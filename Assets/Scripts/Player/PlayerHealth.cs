using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public int healthCount, maxHealth;
    public Text healthCountTxt;
    
	void Start () {
        healthCount = 3;
        maxHealth = 5;
	}
	
	void Update () {
        healthCountTxt.text = healthCount.ToString();
        if (healthCount<0)
            {
                healthCountTxt.text = "0";
            }
    }
}
