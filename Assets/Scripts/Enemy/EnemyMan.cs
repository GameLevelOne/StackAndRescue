using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMan : MonoBehaviour {
    //---------------ENEMY GLOBAL CONFIGURATION---------------//
    public GameObject enemy1, enemy2, enemy3, enemy4, enemy5, enemy6, curtain, snap;
    public Transform spawnP1, spawnP2, spawnP3, spawnP4;
    public int enemyNum, enemyLim;
    public float timer = 5, timeBetween = 3;
    public bool eT2, eT3, eT6;

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
                    SpawnET1(spawnPoint);
                    break;
                case 2 :
                    enemRan = Random.Range(0, 9);
                    if(enemRan<=5)
                        {
                            SpawnET1(spawnPoint);
                        } else
                            {
                                SpawnET2(spawnPoint);
                            }
                    break;
                case 3 :
                    enemRan = Random.Range(0, 9);
                    if(enemRan<=5)
                        {
                            SpawnET1(spawnPoint);
                        } else
                            {
                                SpawnET2(spawnPoint);
                            }
                    break;
                case 4 :
                    enemRan = Random.Range(0, 11);
                    if(enemRan<=5)
                        {
                            SpawnET1(spawnPoint);
                        } else if(enemRan>5 && enemRan<=9)
                            {
                                SpawnET5(spawnPoint);
                            }
                          else if(enemRan>9 && enemRan<=11)
                            {
                                SpawnET2(spawnPoint);
                            }
                    break;
                case 5 :
                    enemRan = Random.Range(0, 9);
                    if(enemRan<=7)
                        {
                            SpawnET5(spawnPoint);
                        } else
                            {
                                SpawnET3(spawnPoint);
                            }
                    break;
                case 6 :
                    enemRan = Random.Range(0, 11);
                    if(enemRan<=5)
                        {
                            SpawnET4(spawnPoint);
                        } else if(enemRan>5 && enemRan<=9)
                            {
                                SpawnET5(spawnPoint);
                            }
                          else if(enemRan>9 && enemRan<=11)
                            {
                                SpawnET2(spawnPoint);
                            }
                    break;
                case 7 :
                    enemRan = Random.Range(0, 11);
                    if(enemRan<=5)
                        {
                            SpawnET1(spawnPoint);
                        } else if(enemRan>5 && enemRan<=9)
                            {
                                SpawnET6(spawnPoint);
                            }
                          else if(enemRan>9 && enemRan<=11)
                            {
                                SpawnET2(spawnPoint);
                            }
                    break;
                case 8 :
                    enemRan = Random.Range(0, 11);
                    if(enemRan<=5)
                        {
                            SpawnET1(spawnPoint);
                        } else if(enemRan>5 && enemRan<=9)
                            {
                                SpawnET4(spawnPoint);
                            }
                          else if(enemRan>9 && enemRan<=11)
                            {
                                SpawnET3(spawnPoint);
                            }
                    break;
                case 9 :
                    enemRan = Random.Range(0, 11);
                    if(enemRan<=5)
                        {
                            SpawnET5(spawnPoint);
                        } else if(enemRan>5 && enemRan<=9)
                            {
                                SpawnET1(spawnPoint);
                            }
                          else if(enemRan>9 && enemRan<=11)
                            {
                                SpawnET6(spawnPoint);
                            }
                    break;
                case 10 :
                    enemRan = Random.Range(0, 21);
                    if(enemRan<=6)
                        {
                            SpawnET1(spawnPoint);
                        } else if(enemRan>6 && enemRan<=9)
                            {
                                SpawnET2(spawnPoint);
                            }
                          else if(enemRan>9 && enemRan<=12)
                            {
                                SpawnET3(spawnPoint);
                            }
                          else if(enemRan>12 && enemRan<=14)
                            {
                                SpawnET4(spawnPoint);
                            }
                          else if(enemRan>14 && enemRan<=18)
                            {
                                SpawnET5(spawnPoint);
                            }
                          else if(enemRan>18 && enemRan<=21)
                            {
                                SpawnET6(spawnPoint);
                            }
                    break;
                default :
                    enemRan = Random.Range(0, 21);
                    if(enemRan<=6)
                        {
                            SpawnET1(spawnPoint);
                        } else if(enemRan>6 && enemRan<=9)
                            {
                                SpawnET2(spawnPoint);
                            }
                          else if(enemRan>9 && enemRan<=12)
                            {
                                SpawnET3(spawnPoint);
                            }
                          else if(enemRan>12 && enemRan<=14)
                            {
                                SpawnET4(spawnPoint);
                            }
                          else if(enemRan>14 && enemRan<=18)
                            {
                                SpawnET5(spawnPoint);
                            }
                          else if(enemRan>18 && enemRan<=21)
                            {
                                SpawnET6(spawnPoint);
                            }
                    break;
            }
        enemyNum++;
    }

    void SpawnET1(Transform spawnPoint) {
        //Enemy Type 1 - Basic
        GameObject enemy = Instantiate(enemy1, spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<EnemyT1>().gameSys = gameSys;
        enemy.GetComponent<EnemyT1>().enemyMan = this;
    }

    void SpawnET2(Transform spawnPoint) {
        //Enemy Type 2 - Shape Shifting
        if (!eT2 && !eT3 && !eT6)
            {
                GameObject enemy = Instantiate(enemy2, spawnPoint.position, spawnPoint.rotation);
                enemy.GetComponent<EnemyT2>().gameSys = gameSys;
                enemy.GetComponent<EnemyT2>().userIn = userIn;
                enemy.GetComponent<EnemyT2>().brickGen = brickGen;
                enemy.GetComponent<EnemyT2>().enemyMan = this;
                eT2 = true;
            }
    }

    void SpawnET3(Transform spawnPoint) {
        //Enemy Type 3 - Size Shifting (Shrink)
        if (!eT3 && !eT2 && !eT6)
            {
                GameObject enemy = Instantiate(enemy3, spawnPoint.position, spawnPoint.rotation);
                enemy.GetComponent<EnemyT3>().gameSys = gameSys;
                enemy.GetComponent<EnemyT3>().userIn = userIn;
                enemy.GetComponent<EnemyT3>().brickGen = brickGen;
                enemy.GetComponent<EnemyT3>().enemyMan = this;
                eT3 = true;
            }
    }

    void SpawnET4(Transform spawnPoint) {
        //Enemy Type 4 - Kamikaze
        GameObject enemy = Instantiate(enemy4, spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<EnemyT4>().gameSys = gameSys;
        enemy.GetComponent<EnemyT4>().enemyMan = this;
    }

    void SpawnET5(Transform spawnPoint) {
        //Enemy Type 5 - Blinding
        GameObject enemy = Instantiate(enemy5, spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<EnemyT5>().gameSys = gameSys;
        enemy.GetComponent<EnemyT5>().curtain = curtain;
        enemy.GetComponent<EnemyT5>().enemyMan = this;
    }

    void SpawnET6(Transform spawnPoint) {
        //Enemy Type 6 - Size Shifting (Enlarge)
        if (!eT3 && !eT2 && !eT6)
            {
                GameObject enemy = Instantiate(enemy6, spawnPoint.position, spawnPoint.rotation);
                enemy.GetComponent<EnemyT6>().gameSys = gameSys;
                enemy.GetComponent<EnemyT6>().userIn = userIn;
                enemy.GetComponent<EnemyT6>().brickGen = brickGen;
                enemy.GetComponent<EnemyT6>().enemyMan = this;
                eT6 = true;
            }
    }
}
