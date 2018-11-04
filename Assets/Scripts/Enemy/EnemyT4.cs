using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT4 : MonoBehaviour {
    //---------------KAMIKAZE ENEMY TYPE---------------//
    public float speed = 8f, defSpeed;
    public GameSystem gameSys;
    public EnemyMan enemyMan;
    public bool onClick;
    public AudioClip fly,xplode;

    private AudioSource audioS;

    //Vector2 tempPos;
    Animator anim;
    Transform target;
    bool dead,frozen;
    
	void Start () {
        anim = GetComponent<Animator>();
        audioS = gameSys.GetComponent<AudioSource>();
        target = gameSys.lastBrick;
        defSpeed = 8f;
        audioS.PlayOneShot(fly);
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
                            if (Vector2.Distance(transform.position, target.position) <= 0.2f)
                                {
                                    speed = 0;
                                    audioS.PlayOneShot(xplode);
                                    if(target!=null && Vector2.Distance(transform.position, target.position) <= 0.2f) 
                                        {
                                            Kamikaze();
                                            dead = true;
                                        }
                                } else
                                    {
                                        speed = defSpeed;
                                        Vector2 dir = target.position - transform.position;
                                        //tempPos = transform.position;
                                        transform.Translate(dir.normalized * speed * Time.deltaTime);
                                    }
                        } else
                            {
                                Kamikaze();
                                if(!dead)
                                    {
                                        audioS.PlayOneShot(xplode);
                                    }
                                dead = true;
                            }
                }
	}

    void Kamikaze() {
        StartCoroutine(Explode());
    }

    IEnumerator Explode() {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        enemyMan.enemyNum--;
        Destroy(gameObject);
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
                audioS.PlayOneShot(xplode);
                dead = true;
                onClick = true;
            }
    }

    void EnemyDead() {
        Destroy(gameObject);
    }
}
