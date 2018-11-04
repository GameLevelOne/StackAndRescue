using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMan : MonoBehaviour {
    //---------------ENEMY GLOBAL CONFIGURATION---------------//
    public GameObject enemy1, enemy2, enemy3, enemy4, enemy5, enemy6, curtain, snap;
    public Transform spawnP1, spawnP2, spawnP3, spawnP4;
    public int enemyNum, enemyLim;
    public float timer = 5, timeBetween = 3;
    public bool eT2, eT3, eT6, killObj;

    GameSystem gameSys;
    Objectives goalSys;
    UserInput userIn;
    BrickGenerator brickGen;
    bool placedBrick;
    int currLevel, enemRan;
    
    void Start () {
        gameSys = GetComponent<GameSystem>();
        goalSys = GetComponent<Objectives>();
        userIn = GetComponent<UserInput>();
        brickGen = GetComponent<BrickGenerator>();
        currLevel = gameSys.level;
	}
	
	void Update () {
        if(!GameObject.FindGameObjectWithTag("enemy"))
            {
                enemyNum = 0;
            }
        if(gameSys.checkpoint.childCount!=0)
            {
                if(gameSys.checkpoint.GetChild(gameSys.checkpoint.childCount-1).tag.Contains("brick"))
                    {
                        placedBrick = true;
                    } else 
                        {
                            placedBrick = false;
                        }
            }
        if(placedBrick && !goalSys.win && !gameSys.gameOver)
            {
                if(timer<=0f)
                    {
                        //timeBetween = Random.Range(3,5); //if you want to randomize spawn interval
                        if(enemyNum<enemyLim && !(enemyNum<0))
                            {
                                int numRan = Random.Range(0, 14);
                                if (numRan <= 1)
                                    {
                                        Spawner(spawnP1);
                                    } else if (numRan <= 5)
                                        {
                                            Spawner(spawnP2);
                                        }
                                    else if (numRan <= 8)
                                        {
                                            Spawner(spawnP3);
                                        }
                                    else if (numRan <= 14)
                                        {
                                            Spawner(spawnP4);
                                        }
                            }
                        timer = timeBetween;
                    }
                timer -= Time.deltaTime;
            }
	}

    public void Spawner(Transform spawnPoint) {
        switch (currLevel)
            {
                case 1 :
                    SpawnET1(spawnPoint,false);
                    break;
                case 2 :
                    enemRan = Random.Range(0, 9);
                    if(enemRan<=5)
                        {
                            SpawnET1(spawnPoint,false);
                        } else
                            {
                                SpawnET2(spawnPoint,false);
                            }
                    break;
                case 3 :
                    enemRan = Random.Range(0, 9);
                    if(enemRan<=5)
                        {
                            SpawnET1(spawnPoint,true);
                        } else
                            {
                                SpawnET2(spawnPoint,false);
                            }
                    break;
                case 4 :
                    enemRan = Random.Range(0, 11);
                    if(enemRan<=5)
                        {
                            SpawnET1(spawnPoint, false);
                        } else if(enemRan>5 && enemRan<=9)
                            {
                                SpawnET5(spawnPoint, false);
                            }
                          else if(enemRan>9 && enemRan<=11)
                            {
                                SpawnET2(spawnPoint, false);
                            }
                    break;
                case 5 :
                    enemRan = Random.Range(0, 9);
                    if(enemRan<=7)
                        {
                            SpawnET5(spawnPoint, true);
                        } else
                            {
                                SpawnET3(spawnPoint, false);
                            }
                    break;
                case 6 :
                    enemRan = Random.Range(0, 11);
                    if(enemRan<=5)
                        {
                            SpawnET4(spawnPoint, true);
                        } else if(enemRan>5 && enemRan<=9)
                            {
                                SpawnET5(spawnPoint, false);
                            }
                          else if(enemRan>9 && enemRan<=11)
                            {
                                SpawnET2(spawnPoint, false);
                            }
                    break;
                case 7 :
                    enemRan = Random.Range(0, 11);
                    if(enemRan<=5)
                        {
                            SpawnET1(spawnPoint, false);
                        } else if(enemRan>5 && enemRan<=9)
                            {
                                SpawnET6(spawnPoint, false);
                            }
                          else if(enemRan>9 && enemRan<=11)
                            {
                                SpawnET2(spawnPoint, false);
                            }
                    break;
                case 8 :
                    enemRan = Random.Range(0, 11);
                    if(enemRan<=5)
                        {
                            SpawnET1(spawnPoint,false);
                        } else if(enemRan>5 && enemRan<=9)
                            {
                                SpawnET4(spawnPoint, true);
                            }
                          else if(enemRan>9 && enemRan<=11)
                            {
                                SpawnET3(spawnPoint, false);
                            }
                    break;
                case 9 :
                    enemRan = Random.Range(0, 11);
                    if(enemRan<=5)
                        {
                            SpawnET5(spawnPoint, false);
                        } else if(enemRan>5 && enemRan<=9)
                            {
                                SpawnET1(spawnPoint, false);
                            }
                          else if(enemRan>9 && enemRan<=11)
                            {
                                SpawnET6(spawnPoint, false);
                            }
                    break;
                case 10 : //Boss Level Phase 1
                    enemRan = Random.Range(0, 11);
                    if(enemRan<=6)
                        {
                            SpawnET1(spawnPoint, false);
                        } else if(enemRan>6 && enemRan<=9)
                            {
                                SpawnET2(spawnPoint, false);
                            }
                          else if(enemRan>9 && enemRan<=11)
                            {
                                SpawnET5(spawnPoint, false);
                            }
                    break;
                case 11 : //Boss Level Phase 2 - Checkpoint 1
                    SpawnET1(spawnPoint, false);
                    break;
                case 12 : //Boss Level Phase 2 - Checkpoint 2
                    enemRan = Random.Range(0, 9);
                    if(enemRan<=6)
                        {
                            SpawnET1(spawnPoint, false);
                        } else if(enemRan>6 && enemRan<=9)
                            {
                                SpawnET6(spawnPoint, false);
                            }
                    break;
                case 13 : //Boss Level Phase 2 - Checkpoint 3
                    enemRan = Random.Range(0, 11);
                    if(enemRan<=6)
                        {
                            SpawnET1(spawnPoint, false);
                        } else if(enemRan>6 && enemRan<=9)
                            {
                                SpawnET6(spawnPoint, false);
                            }
                          else if(enemRan>9 && enemRan<=11)
                            {
                                SpawnET5(spawnPoint, false);
                            }
                    break;
                case 14 : //Boss Level Phase 2 - Checkpoint 4
                    enemRan = Random.Range(0, 11);
                    if(enemRan<=6)
                        {
                            SpawnET1(spawnPoint, false);
                        } else if(enemRan>6 && enemRan<=9)
                            {
                                SpawnET3(spawnPoint, false);
                            }
                          else if(enemRan>9 && enemRan<=11)
                            {
                                SpawnET5(spawnPoint, false);
                            }
                    break;
                case 15 : //Boss Level Phase 2 - Checkpoint 5
                    enemRan = Random.Range(0, 21);
                    if(enemRan<=6)
                        {
                            SpawnET1(spawnPoint, false);
                        } else if(enemRan>6 && enemRan<=9)
                            {
                                SpawnET2(spawnPoint, false);
                            }
                          else if(enemRan>9 && enemRan<=12)
                            {
                                SpawnET3(spawnPoint, false);
                            }
                          else if(enemRan>12 && enemRan<=14)
                            {
                                SpawnET4(spawnPoint, false);
                            }
                          else if(enemRan>14 && enemRan<=18)
                            {
                                SpawnET5(spawnPoint, false);
                            }
                          else if(enemRan>18 && enemRan<=21)
                            {
                                SpawnET6(spawnPoint, false);
                            }
                    break;
                default :
                    enemRan = Random.Range(0, 21);
                    if(enemRan<=6)
                        {
                            SpawnET1(spawnPoint, false);
                        } else if(enemRan>6 && enemRan<=9)
                            {
                                SpawnET2(spawnPoint, false);
                            }
                          else if(enemRan>9 && enemRan<=12)
                            {
                                SpawnET3(spawnPoint, false);
                            }
                          else if(enemRan>12 && enemRan<=14)
                            {
                                SpawnET4(spawnPoint, false);
                            }
                          else if(enemRan>14 && enemRan<=18)
                            {
                                SpawnET5(spawnPoint, false);
                            }
                          else if(enemRan>18 && enemRan<=21)
                            {
                                SpawnET6(spawnPoint, false);
                            }
                    break;
            }
        enemyNum++;
    }

    void SpawnET1(Transform spawnPoint, bool killObj) {
        //Enemy Type 1 - Basic
        GameObject enemy = Instantiate(enemy1, spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<EnemyT1>().gameSys = gameSys;
        enemy.GetComponent<EnemyT1>().enemyMan = this;
        if(killObj)
            {
                enemy.GetComponent<Enemy>().killObj = true;
            }
    }

    void SpawnET2(Transform spawnPoint, bool killObj) {
        //Enemy Type 2 - Shape Shifting
        if (!eT2 && !eT3 && !eT6)
            {
                GameObject enemy = Instantiate(enemy2, spawnPoint.position, spawnPoint.rotation);
                enemy.GetComponent<EnemyT2>().gameSys = gameSys;
                enemy.GetComponent<EnemyT2>().userIn = userIn;
                enemy.GetComponent<EnemyT2>().brickGen = brickGen;
                enemy.GetComponent<EnemyT2>().enemyMan = this;
                eT2 = true;
                if(killObj)
                    {
                        enemy.GetComponent<Enemy>().killObj = true;
                    }
            }
    }

    void SpawnET3(Transform spawnPoint, bool killObj) {
        //Enemy Type 3 - Size Shifting (Shrink)
        if (!eT3 && !eT2 && !eT6)
            {
                GameObject enemy = Instantiate(enemy3, spawnPoint.position, spawnPoint.rotation);
                enemy.GetComponent<EnemyT3>().gameSys = gameSys;
                enemy.GetComponent<EnemyT3>().userIn = userIn;
                enemy.GetComponent<EnemyT3>().brickGen = brickGen;
                enemy.GetComponent<EnemyT3>().enemyMan = this;
                eT3 = true;
                if(killObj)
                    {
                        enemy.GetComponent<Enemy>().killObj = true;
                    }
            }
    }

    public void SpawnET4(Transform spawnPoint, bool killObj) {
        //Enemy Type 4 - Kamikaze
        GameObject enemy = Instantiate(enemy4, spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<EnemyT4>().gameSys = gameSys;
        enemy.GetComponent<EnemyT4>().enemyMan = this;
        if(killObj)
            {
                enemy.GetComponent<Enemy>().killObj = true;
            }
    }

    void SpawnET5(Transform spawnPoint, bool killObj) {
        //Enemy Type 5 - Blinding
        GameObject enemy = Instantiate(enemy5, spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<EnemyT5>().gameSys = gameSys;
        enemy.GetComponent<EnemyT5>().curtain = curtain;
        enemy.GetComponent<EnemyT5>().enemyMan = this;
        if(killObj)
            {
                enemy.GetComponent<Enemy>().killObj = true;
            }
    }

    void SpawnET6(Transform spawnPoint, bool killObj) {
        //Enemy Type 6 - Size Shifting (Enlarge)
        if (!eT3 && !eT2 && !eT6)
            {
                GameObject enemy = Instantiate(enemy6, spawnPoint.position, spawnPoint.rotation);
                enemy.GetComponent<EnemyT6>().gameSys = gameSys;
                enemy.GetComponent<EnemyT6>().userIn = userIn;
                enemy.GetComponent<EnemyT6>().brickGen = brickGen;
                enemy.GetComponent<EnemyT6>().enemyMan = this;
                eT6 = true;
                if(killObj)
                    {
                        enemy.GetComponent<Enemy>().killObj = true;
                    }
            }
    }
}
