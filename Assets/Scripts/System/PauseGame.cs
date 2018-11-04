using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
    public Transform gameSys;
    
	void Update () {
        Pause();
	}

    public void Pause() {
        if(gameObject.activeSelf)
            {
                Time.timeScale = 0;
                gameSys.GetComponent<BoxCollider2D>().enabled = false;
            } else
                {
                    Time.timeScale = 1;
                }
    }
}
