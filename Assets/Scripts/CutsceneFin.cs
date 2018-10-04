using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneFin : MonoBehaviour {
    public GameObject gameSys;

    void Awake() {
        gameSys = GameObject.Find("GameSystem");
    }

    // Use this for initialization
    void Update () {
        gameSys.GetComponent<GameSystem>().CutSceneFinish();
	}
}
