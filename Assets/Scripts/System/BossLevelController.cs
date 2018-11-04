using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLevelController : MonoBehaviour {
    public int timeLeft = 180;
    public Text timeTxt;
    public GameObject witchBoss,princess,witch;
    public Transform spawnPBarr1, spawnPBarr2, spawnPBarr3, spawnPBarr4;
    public float timer, timeLim = 20f;

    GameSystem gameSys;
    Objectives objective;
    EnemyMan enemyMan;

    private void Awake() {
        gameSys = GetComponent<GameSystem>();
        objective = GetComponent<Objectives>();
        enemyMan = GetComponent<EnemyMan>();
    }

    void Start () {
        if(gameSys.level==10)
            {
                StartCoroutine("LoseTime");
            }
	}
	
	void Update () {
        if(gameSys.level==10)
            {
                timeTxt.text = (timeLeft.ToString() + " s");
                if(timeLeft <= 0)
                    {
                        StopCoroutine("LoseTime");
                        timeTxt.text = "Times Up!";
                        objective.lose = true;
                    }
                
                if(gameSys.winOver)
                    {
                        princess.SetActive(false);
                        witch.SetActive(false);
                    }
            }
        if(gameSys.level==11)
            {
                if(objective.checkpointReached==1)
                    {
                        gameSys.cam.GetComponent<CameraControl>().ShakeCam(10f,true);
                        StartCoroutine(Stomp());
                        gameSys.level = 12;
                    }
            }
        if(gameSys.level==12)
            {
                if(objective.checkpointReached==2)
                    {
                        gameSys.cam.GetComponent<CameraControl>().ShakeCam(10f,true);
                        StartCoroutine(Stomp());
                        gameSys.level = 13;
                    }
            }
        if(gameSys.level==13)
            {
                if(objective.checkpointReached==3)
                    {
                        StartCoroutine(DisableSkill());
                        gameSys.level = 14;
                    }
            }
        if(gameSys.level==14)
            {
                if(objective.checkpointReached==4)
                    {    
                        //SpawnBarrage();
                        gameSys.level = 15;
                    }
            }
        if(gameSys.level==15)
            {
                SpawnBarrage();
            }
	}

    IEnumerator LoseTime() {
        while (true)
            {
                yield return new WaitForSeconds(1);
                timeLeft--;
            }
    }

    void SpawnBarrage() {
        if(timeLim>0)
            {
                if(timer<=0f)
                    {
                        if(enemyMan.enemyNum<enemyMan.enemyLim && !(enemyMan.enemyNum <0))
                            {
                                int numRan = Random.Range(0, 14);
                                if (numRan <= 1)
                                    {
                                        enemyMan.SpawnET4(spawnPBarr1,false);
                                    } else if (numRan <= 5)
                                        {
                                            enemyMan.SpawnET4(spawnPBarr2,false);
                                        }
                                    else if (numRan <= 8)
                                        {
                                            enemyMan.SpawnET4(spawnPBarr3,false);
                                        }
                                    else if (numRan <= 14)
                                        {
                                            enemyMan.SpawnET4(spawnPBarr4,false);
                                        }
                            }
                        Debug.Log("SpawnBarr");
                        timer = 1f;
                    }
                timer -= Time.deltaTime;
            }
        timeLim -= Time.deltaTime;
    }

    IEnumerator Stomp() {
        witchBoss.GetComponent<Animator>().SetBool("stomp", true);
        yield return new WaitForSeconds(10f);
        witchBoss.GetComponent<Animator>().SetBool("stomp", false);
    }

    IEnumerator DisableSkill() {
        for (int i = 0; i <= gameSys.skillsAndInds.transform.childCount-1; i++)
            {
                if (gameSys.skillsAndInds.transform.GetChild(i).name.Contains("Skill"))
                    {
                        gameSys.skillsAndInds.transform.GetChild(i).GetComponent<Button>().interactable = false;
                    }
            }
        Debug.Log("Start Disable");
        yield return new WaitForSeconds(30f);
        for (int i = 0; i <= gameSys.skillsAndInds.transform.childCount-1; i++)
            {
                if (gameSys.skillsAndInds.transform.GetChild(i).name.Contains("Skill"))
                    {
                        gameSys.skillsAndInds.transform.GetChild(i).GetComponent<Button>().interactable = true;
                    }
            }
        Debug.Log("DONE");
    }
}
