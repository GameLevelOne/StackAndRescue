using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMan : MonoBehaviour {
    public int coins;
    public Text coinTxt;

	void Update () {
        coinTxt.text = coins.ToString();
	}
}
