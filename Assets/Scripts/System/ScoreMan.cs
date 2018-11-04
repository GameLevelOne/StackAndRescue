using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMan : MonoBehaviour {
    public int coins,accCoins;
    public Text coinTxt;

	void Update () {
        coinTxt.text = coins.ToString();
	}

    public void AddCoins() {
        accCoins = PlayerPrefs.GetInt("coinsSaved");
        PlayerPrefs.SetInt("coinsSaved", accCoins + coins);
    }
}
