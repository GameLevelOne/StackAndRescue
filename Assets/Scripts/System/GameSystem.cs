using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GameSystem : MonoBehaviour {
    public GameObject gameOverScreen,winOverScreen,hero,skillsAndInds,boss1Ed,boss2Ed;
    public Button pauseBtn;
    public bool gameOver,winOver,lastCheck,story;
    public Transform checkpoint, checkPointPrefab, lastBrick, finishPlatf;
    public Camera cam;
    public int level;

    PlayerHealth playerHealthSystem;
    Transform baseLand;

    void Awake () {
        playerHealthSystem = GetComponent<PlayerHealth>();
        baseLand = GetComponent<Objectives>().foundation;
        gameOver = false;
	}

    void Start() {
        if(GetComponent<Objectives>().modeStory)
            {
                story = true;
            }
    }

    void Update () {
        if (playerHealthSystem.healthCount<=0 || GetComponent<Objectives>().lose)
            {
                gameOverScreen.SetActive(true);
                if(gameOverScreen && !gameOver)
                    {
                        GameOver();
                        Destroy(GetComponent<UserInput>().testPos.gameObject);
                        skillsAndInds.SetActive(false);
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
                skillsAndInds.SetActive(false);
                if(level==15) //temporary without cutscene
                    {
                        boss2Ed.SetActive(true);
                    } else if(level==10)
                        {
                            boss1Ed.SetActive(true);
                        }
                    else
                        {
                            if(checkpoint.GetComponent<PlayableDirector>())
                                {
                                    checkpoint.GetComponent<PlayableDirector>().enabled = true;
                                }
                        }
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
    
	public void CheckpointFreeze(bool finish) {
        for(int i=0;i<=checkpoint.childCount-1;i++)
            {
                if(checkpoint.GetChild(i).tag.Contains("brick"))
                    {
                        checkpoint.GetChild(i).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                        checkpoint.GetChild(i).GetComponent<BrickControl>().invincible = true;
                        checkpoint.GetChild(i).GetComponent<Animator>().SetBool("reinf", true);
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
                if(!finish)
                    {
                        if (!lastCheck)
                            {
                                if (GetComponent<Objectives>().checkpointGoal-2== GetComponent<Objectives>().checkpointReached)
                                    {
                                        if(level>=11)
                                            {
                                                checkpoint = Instantiate(finishPlatf, new Vector2(checkpoint.position.x, checkpoint.position.y + 15),checkpoint.rotation);
                                            } else
                                                {
                                                    checkpoint = Instantiate(finishPlatf, new Vector2(checkpoint.position.x, checkpoint.position.y + 16),checkpoint.rotation);
                                                }
                                        lastCheck = true;
                                    } else 
                                        {
                                            if(level>=11)
                                                {
                                                    checkpoint = Instantiate(checkPointPrefab, new Vector2(checkpoint.position.x, checkpoint.position.y + 13),checkpoint.rotation);
                                                } else
                                                    {
                                                        checkpoint = Instantiate(checkPointPrefab, new Vector2(checkpoint.position.x, checkpoint.position.y + 14),checkpoint.rotation);
                                                    }
                                        }
                            }
                    }
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
        winOverScreen.SetActive(true);
        StartCoroutine(ZoomOut());
    }
}
