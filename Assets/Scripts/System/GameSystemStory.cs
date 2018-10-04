using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GameSystemStory : MonoBehaviour {
    public GameObject gameOverScreen,winOverScreen,hero,skillsAndInds;
    public Button pauseBtn;
    public bool gameOver,winOver,lastCheck;
    public Transform checkpoint, checkPointPrefab, lastBrick;
    public Camera cam;

    PlayerHealth playerHealthSystem;
    Transform baseLand;

    void Awake () {
        playerHealthSystem = GetComponent<PlayerHealth>();
        baseLand = GetComponent<Objectives>().foundation;
        gameOver = false;
	}
	
	void Update () {
        if (playerHealthSystem.healthCount<=0)
            {
                gameOverScreen.SetActive(true);
                if(gameOverScreen && !gameOver)
                    {
                        GameOver();
                        Destroy(GetComponent<UserInput>().testPos.gameObject);
                        gameOver = true;
                    }
                StartCoroutine(ZoomOut());
            }
        if(GetComponent<Objectives>().win && !winOver)
            {
                //winOverScreen.SetActive(true);
                pauseBtn.interactable = false;
                GameOver();
                Destroy(GetComponent<UserInput>().testPos.gameObject);
                hero.SetActive(false);
                checkpoint.GetComponent<PlayableDirector>().enabled = true;
                winOver = true;
            }
        if(checkpoint.childCount != 0)
            {
                if (checkpoint.GetChild(checkpoint.childCount - 1).tag.Contains("brick"))
                    {
                        lastBrick = checkpoint.GetChild(checkpoint.childCount - 1);
                    } else
                        {
                            lastBrick = null;
                        }
            } else 
                {
                    lastBrick = null;
                }
	}
    
	public void CheckpointFreeze() {
        for(int i=0;i<=checkpoint.childCount-1;i++)
            {
                if(checkpoint.GetChild(i).tag.Contains("brick"))
                    {
                        checkpoint.GetChild(i).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                        checkpoint.GetChild(i).GetComponent<BrickControl>().invincible = true;
                    }
            }
        //can be utilized for height, limit, etc
        checkpoint.GetComponent<BoxCollider2D>().enabled = false;
        //checkpoint.GetComponent<SpriteRenderer>().enabled = false;
        if(checkpoint.GetComponent<Animator>()!=null)
            {
                checkpoint.GetComponent<Animator>().SetBool("passed", true);
            }
        if (!GetComponent<Objectives>().win)
            { 
                GetComponent<Objectives>().checkpointReached++;
            }
    }

    void GameOver() {
        for(int i=0;i<GameObject.FindGameObjectsWithTag("enemy").Length;i++)
            {
                if(GameObject.FindGameObjectsWithTag("enemy")[i]!=null)
                    { 
                        Destroy(GameObject.FindGameObjectsWithTag("enemy")[i]);
                    }
            }
        //Time.timeScale = 0;
    }

    IEnumerator ZoomOut() {
        while (!baseLand.GetComponent<Renderer>().isVisible)
        {
            cam.orthographicSize += 1;
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator ZoomIn() {
        while (!baseLand.GetComponent<Renderer>().isVisible)
        {
            cam.orthographicSize -= 1;
            yield return new WaitForSeconds(.5f);
        }
    }

    public void CutSceneFinish() {
        skillsAndInds.SetActive(false);
        winOverScreen.SetActive(true);
        StartCoroutine(ZoomOut());
    }
}
