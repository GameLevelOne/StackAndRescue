using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour {
    public Transform gameSys;
    public int heroSkinsNum, princessSkinsNum, brickThemesNum;

    #region Obsolete - Prototype
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
    #endregion
    
    public void CloseGame() {
        Application.Quit();
    }
    

    public void PauseGame() {
        Time.timeScale = 0;
        /*for(int i=0;i<gameSys.childCount;i++)
            { 
                gameSys.GetChild(i).GetComponentInChildren<BoxCollider2D>().enabled = false;
            }*/
        gameSys.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        /*for(int i=0;i<gameSys.childCount;i++)
            { 
                gameSys.GetChild(i).GetComponentInChildren<BoxCollider2D>().enabled = true;
            }*/
        gameSys.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void RestartLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    
    }

    public void ReturnMenu() {
        Time.timeScale = 1;
        if(gameSys.GetComponent<Objectives>().win)
            {
                if(PlayerPrefs.GetInt("levelReached")<gameSys.GetComponent<GameSystem>().level+1)
                    {        
                        PlayerPrefs.SetInt("levelReached", gameSys.GetComponent<GameSystem>().level+1);
                    }
                gameSys.GetComponent<ScoreMan>().AddCoins();
            }
        SceneManager.LoadScene(0);
    }

    public void NextStage() {
        Time.timeScale = 1;
        if(PlayerPrefs.GetInt("levelReached")<gameSys.GetComponent<GameSystem>().level+1)
            {        
                PlayerPrefs.SetInt("levelReached", gameSys.GetComponent<GameSystem>().level+1);
            }
        gameSys.GetComponent<ScoreMan>().AddCoins();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void LoadLevel(int lvl) {
        SceneManager.LoadScene(lvl);
    }

    public void ResetStoryMode() {
        PlayerPrefs.SetInt("levelReached", 1);
        //PlayerPrefs.DeleteAll();
    }

    public void UnlockAllLevels() {
        PlayerPrefs.SetInt("levelReached", 10);
    }

    public void ResetEndless() {
        PlayerPrefs.SetInt("modeNumbers", 1);
    }

    public void UnlockMode() {
        PlayerPrefs.SetInt("modeNumbers", 2);
    }

    public void ResetSkins() {
        for(int i=0;i<heroSkinsNum;i++)
            {
                PlayerPrefs.SetInt("heroSkin"+(i+1),0);
            }
        for(int i=0;i<heroSkinsNum;i++)
            {
                PlayerPrefs.SetInt("princessSkin"+(i+1),0);
            }
        for(int i=0;i<heroSkinsNum;i++)
            {
                PlayerPrefs.SetInt("brickTheme"+(i+1),0);
            }
    }

    public void ResetCoins() {
        PlayerPrefs.SetInt("coinsSaved", 0);
    }

    public void UnlimitedCoins() {
        PlayerPrefs.SetInt("coinsSaved", 99999);
    }

    public void ResetPotions() {
        PlayerPrefs.SetInt("manaCharge1", 0);
        PlayerPrefs.SetInt("manaCharge2", 0);
        PlayerPrefs.SetInt("manaCharge3", 0);
    }
}
