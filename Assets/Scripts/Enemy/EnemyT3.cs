using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT3 : MonoBehaviour {
    //---------------SHRINKING ENEMY TYPE---------------//
    public float speed = 2f, defSpeed;
    public GameSystem gameSys;
    public UserInput userIn;
    public BrickGenerator brickGen;
    public EnemyMan enemyMan;
    public int health;

    Vector2 tempPos,minCam,maxCam;
    Animator anim;
    Transform target;
    bool dead,inPos,frozen;//att;
    float timer = 2f;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        target = userIn.testPos;
        minCam = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxCam = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        defSpeed = 2f;
        health = 2;
    }
	
	// Update is called once per frame
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
                    anim.SetBool("frozen", false);
                    frozen = false;
                    //anim.speed = 1;
                    if(target != null && !dead) 
                        { 
                            if (transform.position.x >= minCam.x+4 && transform.position.x <= maxCam.x-4 &&
                                transform.position.y >= minCam.y+4 && transform.position.y <= maxCam.y-4)
                                {
                                    speed = 0;
                                    /*if(!att) //attack only once
                                        {
                                            int rand = Random.Range(0,5);
                                            if(rand==0)
                                                {
                                                    target.transform.localScale = new Vector3(1.6f,1.6f,1);
                                                } else
                                                    {
                                                        target.transform.localScale = new Vector3(.4f,.4f,1);
                                                    }
                                            att = true;
                                        }*/
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

    /*void ShrinkAndLarge() {
        StartCoroutine(ManipulateSize());
    }

    IEnumerator ManipulateSize() {
        if (!att) 
            {
                int rand = Random.Range(0, 5);
                if (rand <= 1)
                    {
                        target.transform.localScale = new Vector3(1.6f, 1.6f, 1);
                    } else if (rand <= 3)
                        {
                            target.transform.localScale = new Vector3(.4f, .4f, 1);
                        }
                    else
                        {
                            target.transform.localScale = new Vector3(.8f, .8f, 1);
                        }
                att = true;
                yield return new WaitForSeconds(1.5f);
                att = false;
            }
    }*/

    void ShrinkAndLarge() {
        target.GetComponent<Animator>().SetTrigger("glow");
        int rand = Random.Range(0, 5);
        /*if (rand <= 1)
            {
                target.transform.localScale = new Vector3(1.2f, 1.2f, 1);
            } else if (rand <= 3)
                {
                    target.transform.localScale = new Vector3(.4f, .4f, 1);
                }
            else
                {
                    target.transform.localScale = new Vector3(.8f, .8f, 1);
                }*/
        if (rand <= 2)
            {
                target.transform.localScale = new Vector3(.6f, .6f, 1);
            } else
                {
                    target.transform.localScale = new Vector3(.8f, .8f, 1);
                }
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
                            gameSys.GetComponent<Objectives>().enemyKilled++;
                            gameSys.GetComponent<PlayerSP>().spCount++;
                            anim.SetTrigger("dead");
                            dead = true;
                        }
            }
    }

    void EnemyDead() {
        enemyMan.eT3 = false;
        Destroy(gameObject);
    }
}
