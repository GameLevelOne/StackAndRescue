using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour {
    public Transform gameSys;
	
	public void StartBuildDemo () {
        SceneManager.LoadScene(1);
	}

	public void StartDefendDemo () {
        SceneManager.LoadScene(2);
	}

	public void StartEndlessDemo () {
        SceneManager.LoadScene(3);
	}
    
	public void StartProtoDemo () {
        SceneManager.LoadScene(4);
	}

    public void CloseGame() {
        Application.Quit();
    }

    public void PauseGame() {
        Time.timeScale = 0;
        for(int i=0;i<gameSys.childCount;i++)
            { 
                gameSys.GetChild(i).GetComponentInChildren<BoxCollider2D>().enabled = false;
            }
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        for(int i=0;i<gameSys.childCount;i++)
            { 
                gameSys.GetChild(i).GetComponentInChildren<BoxCollider2D>().enabled = true;
            }
    }

    public void ReturnMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
