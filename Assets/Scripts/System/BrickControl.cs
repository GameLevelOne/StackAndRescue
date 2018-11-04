using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BrickControl : MonoBehaviour {
    public GameObject gameSys;
    public Transform healthInd;
    public int brickHealth;
    public bool invincible,dropped,abyssed;
    public Animator anim;
    public SpriteRenderer sprite;
    
    private bool destroyed,check,fin;
    private float timer = .5f;

    void Start() {
        brickHealth = 2;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (anim.GetBool("reinf"))
            {
                if (timer<=0)
                    {
                        anim.SetBool("reinf", false);
                        timer = 0;
                    }
                timer -= Time.deltaTime;
            }
            
        if(brickHealth==1)
            {
                anim.SetBool("dam", true);
            } else if(brickHealth<=0)
                {
                    if(!destroyed)
                        {
                            anim.SetBool("dam", false);
                            anim.SetTrigger("des");
                            destroyed = true;
                        }
                }
    }

    void OnMouseDown() {
        if(!dropped && !abyssed)
            {
                if(!EventSystem.current.IsPointerOverGameObject())
                    {
                        transform.Rotate(0, 0, 90);
                    }
            }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if(coll.gameObject.tag.Contains("brick") || coll.gameObject.tag.Contains("land"))
            { 
                if(!gameSys.GetComponent<GameSystem>().gameOver) 
                    {
                        if(!dropped)
                            {
                                gameSys.GetComponent<GameSystem>().cam.GetComponent<CameraControl>().ShakeCam(.25f,false);
                                gameSys.GetComponent<BrickGenerator>().SpawnNextBrick(); //Spawning when touching other brick
                                Vector3 scale = transform.localScale;
                                transform.SetParent(gameSys.GetComponent<GameSystem>().checkpoint);
                                if(gameSys.GetComponent<GameSystem>().checkpoint.name.Contains("finishPlatf"))
                                    {
                                        transform.localScale = scale/2;
                                    } else
                                        { 
                                            if(!gameSys.GetComponent<GameSystem>().lastCheck)
                                                {
                                                    transform.localScale = scale/1.25f; //divide by 1.25 if you want to use fairy marker
                                                } else
                                                    {
                                                        transform.localScale = scale/2;
                                                    }
                                        }
                                transform.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                                //gameSys.GetComponent<ScoreMan>().coins += 5;
                                sprite.sortingOrder = -1;
                                dropped = true;
                            }
                    }
            }
        if(coll.gameObject.tag.Contains("abyss"))
            {
                if(!gameSys.GetComponent<GameSystem>().gameOver || !gameSys.GetComponent<Objectives>().win) 
                    {
                        if(!dropped)
                            {
                                gameSys.GetComponent<BrickGenerator>().SpawnNextBrick();
                                transform.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                                sprite.sortingOrder = -1;
                                dropped = true;
                            }
                        if(!abyssed)
                            {
                                if(gameSys.GetComponent<PlayerHealth>().healthCount!=0)
                                    {
                                        gameSys.GetComponent<PlayerHealth>().healthCount -= 1;
                                        Destroy(healthInd.GetChild(healthInd.childCount-1).gameObject); //Desuteroying 1 health indicator
                                    }
                                if(dropped)
                                    {
                                        transform.SetParent(null);
                                    }
                                abyssed = true;
                            }
                    }
            }
    }

    void OnTriggerEnter2D(Collider2D trigg) {
        if(trigg.tag.Contains("cleaner")) //When the ground is no longer visible, there will be invisible collider taking its role
            {
                if(!gameSys.GetComponent<GameSystem>().gameOver || !gameSys.GetComponent<Objectives>().win) 
                    {
                        if(!dropped)
                            {
                                gameSys.GetComponent<BrickGenerator>().SpawnNextBrick();
                                dropped = true;
                            }
                        if(!abyssed)
                            {
                                if(gameSys.GetComponent<PlayerHealth>().healthCount!=0)
                                    {
                                        gameSys.GetComponent<PlayerHealth>().healthCount -= 1;
                                        Destroy(healthInd.GetChild(healthInd.childCount-1).gameObject);
                                    }
                                if(dropped)
                                    {
                                        transform.SetParent(null);
                                    }
                                abyssed = true;
                            }
                    }
            }
    }

    void OnTriggerStay2D(Collider2D trigg) {
        if(trigg.tag.Contains("checkpoint"))
            {
                if(!gameSys.GetComponent<GameSystem>().gameOver) 
                    {
                        if(dropped)
                            {
                                //gameSys.GetComponent<GameSystem>().CheckpointFreeze();
                                if(!check)
                                    { 
                                        if(trigg.name.Contains("finishPlatf"))
                                            {
                                                fin = true;   
                                            } else
                                                {
                                                    fin = false;
                                                }
                                        if(!gameSys.GetComponent<GameSystem>().lastCheck)
                                            {
                                                StartCoroutine(Checkpoint(fin));
                                            } else
                                                {
                                                    sprite.sortingOrder = 0;
                                                    gameSys.GetComponent<GameSystem>().CheckpointFreeze(fin);
                                                }
                                        check = true;
                                    }
                            }
                    }
            }
        if(trigg.tag.Contains("hardening")) //Shining seterong block
            {
                if(!gameSys.GetComponent<GameSystem>().gameOver) 
                    {
                        if(!abyssed)
                            {
                                if(dropped)
                                    {
                                        StartCoroutine(Hardened());
                                    }
                            }
                    }
            }
        if(trigg.tag.Contains("explode")) //When there is kamikaze attack
            {
                if(!trigg.GetComponentInParent<EnemyT4>().onClick)
                    {
                        if(dropped)
                            {   
                                if(!invincible)
                                    {
                                        if(!destroyed)
                                            {
                                                anim.SetTrigger("des");
                                                destroyed = true;
                                            }
                                    }
                            }
                    }
            }
    }

    void OnTriggerExit2D(Collider2D trigg) {
        if(trigg.tag.Contains("cleaner"))
            {
                Destroy(gameObject);
            }
    }

    void Desutroy() {
        Destroy(gameObject);
    }

    IEnumerator Hardened() {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        invincible = true;
        anim.SetBool("reinfSkill", true);
        yield return new WaitForSeconds(5);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        invincible = false;
        anim.SetBool("reinfSkill", false);
    }

    IEnumerator Checkpoint(bool f) {
        yield return new WaitForSeconds(1);
        gameSys.GetComponent<GameSystem>().CheckpointFreeze(f);
    }
}
