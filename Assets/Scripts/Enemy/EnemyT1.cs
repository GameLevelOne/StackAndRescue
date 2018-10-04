using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT1 : MonoBehaviour {
    //---------------BASIC ENEMY TYPE---------------//
    public float speed = 2f, defSpeed;
    public int damage;
    public GameSystem gameSys;
    public EnemyMan enemyMan;

    Vector2 tempPos;
    Animator anim;
    Transform target;
    bool dead,frozen;
    
	void Start () {
        anim = GetComponent<Animator>();
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
                            if (Vector2.Distance(transform.position, target.position) <= 0.2f)
                                {
                                    speed = 0;
                                    if(target!=null && Vector2.Distance(transform.position, target.position) <= 0.2f) 
                                        {
                                            anim.SetTrigger("attack");
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
                gameSys.GetComponent<Objectives>().enemyKilled++;
                gameSys.GetComponent<PlayerSP>().spCount++;
                anim.SetTrigger("dead");
                dead = true;
            }
    }

    void EnemyDead() {
        Destroy(gameObject);
    }
}
