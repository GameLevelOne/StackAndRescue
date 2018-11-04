using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT6 : MonoBehaviour {
    //---------------ENLARGING ENEMY TYPE---------------//
    public float speed = 2f, defSpeed;
    public GameSystem gameSys;
    public UserInput userIn;
    public BrickGenerator brickGen;
    public EnemyMan enemyMan;
    public int health;
    public AudioClip spell;

    private AudioSource audioS;
    private Vector2 tempPos,minCam,maxCam;
    private Animator anim;
    private Transform target;
    private bool dead,inPos,frozen;//att;
    private float timer = 2f;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        audioS = gameSys.GetComponent<AudioSource>();
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
                                            audioS.PlayOneShot(spell);
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
        if (rand <= 2)
            {
                target.transform.localScale = new Vector3(1.1f, 1.1f, 1);
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
        enemyMan.eT6 = false;
        Destroy(gameObject);
    }
}
