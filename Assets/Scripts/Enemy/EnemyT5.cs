using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT5 : MonoBehaviour {
    //---------------BLINDING ENEMY TYPE---------------//
    public float speed = 2f, defSpeed, runSpeed;
    public GameSystem gameSys;
    public EnemyMan enemyMan;
    public GameObject curtain;
    public AudioClip squash;

    private AudioSource audioS;
    private Vector2 tempPos,minCam,maxCam, newTarget;
    private Animator anim;
    private Transform target;
    private float timer = 10f;
    private bool dead,inPos,timeOut,frozen;
    
	void Start () {
        anim = GetComponent<Animator>();
        audioS = gameSys.GetComponent<AudioSource>();
        target = gameSys.lastBrick;
        minCam = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxCam = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        defSpeed = 5f;
        runSpeed = 7f;
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
                    if (timer<=0 && !dead) 
                        {
                            if (transform.position.x < minCam.x - 1 || transform.position.x > maxCam.x + 1 ||
                                transform.position.y < minCam.y - 1 || transform.position.y > maxCam.y + 1)
                                {
                                    enemyMan.enemyNum--;
                                    Destroy(gameObject);
                                    dead = true;
                                } else
                                    {
                                        if (!timeOut)
                                            {
                                                float randX = Random.Range(minCam.x-10,maxCam.x+10);
                                                float randY = Random.Range(maxCam.y+1,maxCam.y+10);
                                                newTarget = new Vector2(randX,randY); 
                                                timeOut=true;
                                            } else
                                                {
                                                    MoveOutsideScreen();
                                                }
                                     }
                         } else
                            {
                                if (target != null && !dead)
                                    {
                                        if (!inPos)
                                            {
                                                if (transform.position.x >= minCam.x+2 && transform.position.x <= maxCam.x-2 &&
                                                    transform.position.y >= minCam.y+2 && transform.position.y <= maxCam.y-2)
                                                    {
                                                        float randX = Random.Range(minCam.x,maxCam.x);
                                                        float randY = Random.Range(minCam.y,maxCam.y);
                                                        newTarget = new Vector2(randX,randY);
                                                        MoveToNewTarget();
                                                        inPos = true;
                                                    } else
                                                        {
                                                            MoveToTarget();
                                                        }
                                            } else 
                                                {
                                                    if (Vector2.Distance(transform.position, newTarget) <= .2f)
                                                        {
                                                            float randX = Random.Range(minCam.x,maxCam.x);
                                                            float randY = Random.Range(minCam.y,maxCam.y);
                                                            newTarget = new Vector2(randX,randY);
                                                            MoveToNewTarget();
                                                        } else
                                                            {
                                                                MoveToNewTarget();
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
                                                            if (newTarget == Vector2.zero)
                                                                {
                                                                    float randX = Random.Range(minCam.x,maxCam.x);
                                                                    float randY = Random.Range(minCam.y,maxCam.y);
                                                                    newTarget = new Vector2(randX,randY);
                                                                    MoveToNewTarget();
                                                                } else
                                                                    {
                                                                        MoveToNewTarget();
                                                                    }
                                                        }
                                            }
                            }
                    timer -= Time.deltaTime;
                }
	}

    void MoveToTarget() {
        speed = defSpeed;
        Vector2 dir = target.position - transform.position;
        tempPos = transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        MoveAnim();
    }

    void MoveToNewTarget() {
        speed = defSpeed;
        Vector2 dir = new Vector2((newTarget.x - transform.position.x), (newTarget.y - transform.position.y));
        tempPos = transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        MoveAnim();
    }

    void MoveOutsideScreen() {
        speed = runSpeed;
        Vector2 dir = new Vector2((newTarget.x - transform.position.x), (newTarget.y - transform.position.y));
        tempPos = transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        MoveAnim();
    }

    void MoveAnim() {
        if (transform.position.x < tempPos.x)
            {
                anim.SetBool("flyLeft", true);
                anim.SetBool("flyRight", false);
            }
        if (transform.position.x > tempPos.x)
            {
                anim.SetBool("flyRight", true);
                anim.SetBool("flyLeft", false);
            }
    }

    void OnMouseDown() {
        if (gameSys.level == 5)
            {
                gameSys.GetComponent<Objectives>().lose = true;
            }
        if(!dead)
            {
                target = null;
                newTarget = transform.position;
                speed = 0;
                enemyMan.enemyNum--;
                if(GetComponent<Enemy>().killObj)
                    {
                        gameSys.GetComponent<Objectives>().enemyKilled++;
                    }
                if(gameSys.GetComponent<PlayerSP>().spCount<50)
                    {
                        gameSys.GetComponent<PlayerSP>().spCount+=5;
                    }
                anim.SetTrigger("dead");
                dead = true;
                audioS.PlayOneShot(squash);
            }
    }

    void EnemyDead() {
        StartCoroutine(BlindingDark());
    }

    IEnumerator BlindingDark() {
        curtain.SetActive(true);
        yield return new WaitForSeconds(2);
        curtain.SetActive(false);
        Destroy(gameObject);
    }

    void EnemyDie() {
        Destroy(gameObject);
    }
}
