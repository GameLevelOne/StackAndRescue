using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSP : MonoBehaviour {
    public int spCount;
    public Text spCountTxt;
    public Slider spInd;
    
	void Start () {
        spCount = 50;
	}
	
	void Update () {
        spCountTxt.text = spCount.ToString();
        if(spCount>50)
            {
                spCount = 50;
            }
        spInd.value = spCount;
        if (spCount<0)
            {
                spCountTxt.text = "0";
            }
        if (spCount>50)
            {
                spCountTxt.text = "50";
            }
	}
}
