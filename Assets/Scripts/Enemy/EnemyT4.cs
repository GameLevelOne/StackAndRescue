using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT4 : MonoBehaviour {
    //---------------KAMIKAZE ENEMY TYPE---------------//
    public float speed = 8f, defSpeed;
    public GameSystem gameSys;
    public EnemyMan enemyMan;
    public bool onClick;

    Vector2 tempPos;
    Animator anim;
    Transform target;
    bool dead,frozen;
    
	void Start () {
        anim = GetComponent<Animator>();
        target = gameSys.lastBrick;
        defSpeed = 8f;
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
                                    if(target!=null && Vector2.Distance(transform.position, target.position) <= 0.2f) 
                                        {
                                            Kamikaze();
                                            dead = true;
                                        }
                                } else
                                    {
                                        speed = defSpeed;
                                        Vector2 dir = target.position - transform.position;
                                        tempPos = transform.position;
                                        transform.Translate(dir.normalized * speed * Time.deltaTime);
                                    }
                        } else
                            {
                                Kamikaze();
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
                gameSys.GetComponent<Objectives>().enemyKilled++;
                gameSys.GetComponent<PlayerSP>().spCount++;
                anim.SetTrigger("dead");
                dead = true;
                onClick = true;
            }
    }

    void EnemyDead() {
        Destroy(gameObject);
    }
}
