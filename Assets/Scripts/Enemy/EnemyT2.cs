using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT2 : MonoBehaviour {
    //---------------BLOCK SHIFTING ENEMY TYPE---------------//
    public float speed = 2f, defSpeed;
    public GameSystem gameSys;
    public UserInput userIn;
    public BrickGenerator brickGen;
    public EnemyMan enemyMan;
    public GameObject brickJ, brickL, brickLong, brickS, brickZ, brickSquare, brickT, snap;
    public int health;

    Vector2 tempPos,minCam,maxCam;
    Animator anim;
    Transform target;
    bool dead,inPos,frozen/*,att,direct*/;
    float timer = 2f;
    
	void Start () {
        anim = GetComponent<Animator>();
        target = userIn.testPos;
        minCam = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxCam = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        defSpeed = 2f;
        brickJ = brickGen.brickJ;
        brickL = brickGen.brickL;
        brickLong = brickGen.brickLong;
        brickS = brickGen.brickS;
        brickSquare = brickGen.brickSquare;
        brickT = brickGen.brickT;
        brickZ = brickGen.brickZ;
        snap = enemyMan.snap;
        health = 2;
    }
	
	void Update () {
        target = userIn.testPos;
        if(GetComponent<Enemy>().freezed)
            {
                speed = 0;
                if(!frozen)
                    {
                        anim.SetBool("frozen", true);
                        frozen = true;
                    }
                /*if(!dead)
                    {
                        anim.speed = 0;
                    } else
                        {
                            anim.speed = 1;
                        }*/
            } else 
                { 
                    //anim.speed = 1;
                    anim.SetBool("frozen", false);
                    frozen = false;
                    if(target != null && !dead) 
                        { 
                            if (transform.position.x >= minCam.x+4 && transform.position.x <= maxCam.x-4 &&
                                transform.position.y >= minCam.y+4 && transform.position.y <= maxCam.y-4)
                                {
                                    speed = 0;
                                    if(timer<=0)
                                        {
                                            anim.SetTrigger("attack");
                                            timer = 2f;
                                        }
                                    timer-=Time.deltaTime;
                                    inPos = true;
                                } else
                                    {
                                        inPos = false;
                                        if(!inPos)
                                            { 
                                                speed = defSpeed;
                                                Vector2 dir = target.position - transform.position;
                                                tempPos = transform.position;
                                                transform.Translate(dir.normalized * speed * Time.deltaTime);
                                                if(transform.position.x<tempPos.x)
                                                    {
                                                        anim.SetBool("flyLeft", true);
                                                        anim.SetBool("flyRight", false);
                                                    }
                                                if(transform.position.x>tempPos.x)
                                                    {
                                                        anim.SetBool("flyRight", true);
                                                        anim.SetBool("flyLeft", false);
                                                    }
                                            }
                                    }
                        } else
                            {
                                speed = 0;
                            }
                }
	}

    /*void ShiftBlock() {
        StartCoroutine(ManipulateShape());
    }

    IEnumerator ManipulateShape() {
        if (!att) 
            {
                GameObject changedBrick = Instantiate(GetRandomBrick(), userIn.testPos.position, Quaternion.identity);
                changedBrick.transform.localScale = new Vector3(.8f, .8f, 1);
                changedBrick.GetComponent<BrickControl>().gameSys = brickGen.gameObject;
                changedBrick.GetComponent<BrickControl>().healthInd = brickGen.healthInd;
                Destroy(userIn.testPos.gameObject);
                userIn.testPos = changedBrick.transform;
                att = true;
                yield return new WaitForSeconds(2f);
                att = false;
            }
    }*/

    void ShiftBlock() {
        GameObject s = Instantiate(snap);
        s.transform.position = userIn.testPos.position;
        GameObject changedBrick = Instantiate(GetRandomBrick(), userIn.testPos.position, Quaternion.identity);
        changedBrick.transform.localScale = new Vector3(.8f, .8f, 1);
        changedBrick.GetComponent<BrickControl>().gameSys = brickGen.gameObject;
        changedBrick.GetComponent<BrickControl>().healthInd = brickGen.healthInd;
        Destroy(userIn.testPos.gameObject);
        userIn.testPos = changedBrick.transform;
    }

    GameObject GetRandomBrick () {
        GameObject randomBrick = null;
        int randNum = Random.Range(1, 7);
        switch (randNum)
            {
                case 1 :
                    randomBrick = brickJ;
                    break;
                case 2 :
                    randomBrick = brickL;
                    break;
                case 3 :
                    randomBrick = brickLong;
                    break;
                case 4 :
                    randomBrick = brickS;
                    break;
                case 5 :
                    randomBrick = brickSquare;
                    break;
                case 6 :
                    randomBrick = brickT;
                    break;
                case 7 :
                    randomBrick = brickZ;
                    break;
            }
        return randomBrick;
    }

    void OnMouseDown() {
        if(!dead)
            {
                if(health>1)
                    {
                        health--;
                        anim.SetTrigger("hit");
                    } else
                        {
                            target = null;
                            speed = 0;
                            enemyMan.enemyNum--;
                            gameSys.GetComponent<ScoreMan>().coins += 5;
                            if(GetComponent<Enemy>().killObj)
                                {
                                    gameSys.GetComponent<Objectives>().enemyKilled++;
                                }
                            if(gameSys.GetComponent<PlayerSP>().spCount<50)
                                {
                                    gameSys.GetComponent<PlayerSP>().spCount++;
                                }
                            anim.SetTrigger("dead");
                            dead = true;
                        }
            }
    }

    void EnemyDead() {
        enemyMan.eT2 = false;
        Destroy(gameObject);
    }
}
