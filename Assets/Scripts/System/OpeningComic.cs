using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningComic : MonoBehaviour {
    //----------Opening & Ending Controller----------//
    public Animator anim;
    public GameObject storymenu, levelLoader,gameSys;
    public AudioManager audioMan;
    public AudioClip mainMenuBGM,boss2BGM;
    public bool bossOp1,bossEd1,bossOp2,bossEd2;
    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
        anim = GetComponentInParent<Animator>();
        if(bossOp2)
            {
                audioSource = GetComponent<AudioSource>();
                audioSource.volume = 1;
                audioSource.clip = boss2BGM;
                audioSource.Play();
            }
	}

    void OnMouseDown() {
        anim.SetTrigger("skip");
    }

    void OnDisable() {
        if(!bossOp1 && !bossEd1 && !bossOp2 && !bossEd2)
            {
                storymenu.SetActive(true);
                audioMan.BackgroundMusic(mainMenuBGM);
            } else if(bossOp1)
                {
                    levelLoader.GetComponent<LevelLoader>().LoadLevel(10);
                }
            else if(bossEd1 || bossEd2)
                {
                    gameSys.GetComponent<GameSystem>().winOverScreen.SetActive(true);
                }
    }

    public void Skip() {
        anim.SetTrigger("end");
    }
}
