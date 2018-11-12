using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasedItems : MonoBehaviour {
    public GameObject potion1Ind, potion2Ind, potion3Ind;
    public Text potion1Txt, potion2Txt, potion3Txt;
    public PlayerSP playerSP;

	// Use this for initialization
	void Start () {
        playerSP = GetComponent<PlayerSP>();
		if(PlayerPrefs.HasKey("manaCharge1"))
            {
                potion1Txt.text = PlayerPrefs.GetInt("manaCharge1").ToString();
            }
		if(PlayerPrefs.HasKey("manaCharge2"))
            {
                potion2Txt.text = PlayerPrefs.GetInt("manaCharge2").ToString();
            }
		if(PlayerPrefs.HasKey("manaCharge3"))
            {
                potion3Txt.text = PlayerPrefs.GetInt("manaCharge3").ToString();
            }
	}
	
	public void UsePotion(string potionType) {
        if(PlayerPrefs.GetInt(potionType)>0 && !(playerSP.spCount>=50))
            {
                if(potionType=="manaCharge1")
                    {
                        playerSP.spCount += 5;
                        int currPot = PlayerPrefs.GetInt(potionType)-1;
                        PlayerPrefs.SetInt(potionType,currPot);
                        potion1Txt.text = PlayerPrefs.GetInt("manaCharge1").ToString();
            } else if(potionType=="manaCharge2")
                        {
                            playerSP.spCount += 10;
                            int currPot = PlayerPrefs.GetInt(potionType)-1;
                            PlayerPrefs.SetInt(potionType,currPot);
                            potion2Txt.text = PlayerPrefs.GetInt("manaCharge2").ToString();
                        }
                    else if(potionType=="manaCharge3")
                        {
                            playerSP.spCount += 30;
                            int currPot = PlayerPrefs.GetInt(potionType)-1;
                            PlayerPrefs.SetInt(potionType,currPot);
                            potion3Txt.text = PlayerPrefs.GetInt("manaCharge3").ToString();
                        }
            }
    }
}
