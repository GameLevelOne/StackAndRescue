using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT1 : MonoBehaviour {
    //---------------BASIC ENEMY TYPE---------------//
    public float speed = 2f, defSpeed;
    public int damage;
    public GameSystem gameSys;
    public EnemyMan enemyMan;
    public AudioClip att;

    private AudioSource audioS;
    private Vector2 tempPos;
    private Animator anim;
    private Transform target;
    private bool dead,frozen;
    private int attNum;
    private float timer = 1.5f;
    
	void Start () {
        anim = GetComponent<Animator>();
        audioS = gameSys.GetComponent<AudioSource>();
        target = gameSys.lastBrick;
        defSpeed = 2f;
        damage = 1;
	}
	
	void Update () {
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
                            timer-=Time.deltaTime;
                            if (Vector2.Distance(transform.position, target.position) <= 0.2f)
                                {
                                    speed = 0;
                                    if(target!=null && Vector2.Distance(transform.position, target.position) <= 0.2f) 
                                        {
                                            if(timer<=0)
                                                {
                                                    anim.SetTrigger("attack");
                                                    attNum++;
                                                    if(gameSys.level == 4)
                                                        {
                                                            if(attNum==2)
                                                                {
                                                                    gameSys.GetComponent<Objectives>().lose = true;
                                                                }
                                                        }
                                                    timer = 1.5f;
                                                }
                                        }
                                } else
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
                        } else
                            {
                                if(gameSys.checkpoint.childCount != 0 && !dead)
                                    {
                                        target = gameSys.lastBrick;
                                        speed = defSpeed;
                                    } else
                                        {
                                            speed = 0;
                                        }
                            }
                }
	}

    void Attack() {
        if(target!=null) 
            { 
                StartCoroutine(WaitAttAnim());
                if(!target.GetComponent<BrickControl>().invincible)
                    {
                        target.GetComponent<BrickControl>().brickHealth-=damage;
                    }
                audioS.PlayOneShot(att);
            }
    }

    IEnumerator WaitAttAnim() {
        yield return new WaitForSeconds(1);
    }

    void OnMouseDown() {
        if(!dead)
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

    void EnemyDead() {
        Destroy(gameObject);
    }
}
