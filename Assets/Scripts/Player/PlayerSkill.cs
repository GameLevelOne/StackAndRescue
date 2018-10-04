using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour {
    public GameSystem gameSys;
    public PlayerSP playerSP;
    public EnemyMan enemyMan;
    public PlayerHealth playerHealth;
    public int sp,costDestroy,costHardening,costFreeze,costAnnihilate,costHeal;
    public GameObject healthInd,healthIndPrefab,hardeningArea,annihilateFX,freezeFX;
    
	void Start () {
        gameSys = GetComponent<GameSystem>();
        playerSP = GetComponent<PlayerSP>();
        enemyMan = GetComponent<EnemyMan>();
        playerHealth = GetComponent<PlayerHealth>();
        costDestroy = 5;
        costHardening = 15;
        costFreeze = 25;
        costAnnihilate = 45;
        costHeal = 50;
	}
	
	void Update () {
        sp = playerSP.spCount;
	}

    public void SkillDestroyLast() {
        if(sp-costDestroy>=0)
            {
                if(gameSys.lastBrick!=null)
                    {
                        //playerSP.spCount = sp - costDestroy;
                        gameSys.lastBrick.GetComponent<Animator>().SetTrigger("des");
                        //Destroy(gameSys.lastBrick.gameObject);
                    }
            }
    }

    public void SkillHardening() {
        if(sp-costHardening>=0)
            {
                //playerSP.spCount = sp - costHardening;
                StartCoroutine(Hardening());
            }
    }

    IEnumerator Hardening() {
        hardeningArea.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        hardeningArea.gameObject.SetActive(false);
    }

    public void SkillFreeze() {
        if(sp-costFreeze>=0)
            {
                //playerSP.spCount = sp - costFreeze;
                StartCoroutine(Freeze());
            }
    }

    IEnumerator Freeze() {
        for(int i=0;i<GameObject.FindGameObjectsWithTag("enemy").Length;i++)
            {
                if(GameObject.FindGameObjectsWithTag("enemy")[i]!=null)
                    {
                        GameObject g = GameObject.FindGameObjectsWithTag("enemy")[i];
                        g.GetComponent<Enemy>().freezed = true;
                        GameObject h = Instantiate(freezeFX);
                        h.transform.position = g.transform.position;
                    }
            }
        yield return new WaitForSeconds(5f);
        for (int i=0;i<GameObject.FindGameObjectsWithTag("enemy").Length;i++)
            {
                if(GameObject.FindGameObjectsWithTag("enemy")[i]!=null)
                    {
                        GameObject.FindGameObjectsWithTag("enemy")[i].GetComponent<Enemy>().freezed = false;
                    }
            }
    }

    public void SkillAnnihillation() {
        if(sp-costAnnihilate>=0)
            {
                //playerSP.spCount = sp - costAnnihilate;
                annihilateFX.SetActive(true);
                for(int i=0;i<GameObject.FindGameObjectsWithTag("enemy").Length;i++)
                    {
                        if(GameObject.FindGameObjectsWithTag("enemy")[i]!=null)
                            { 
                                enemyMan.enemyNum--;
                                GameObject.FindGameObjectsWithTag("enemy")[i].GetComponent<Animator>().SetTrigger("die");
                                //Destroy(GameObject.FindGameObjectsWithTag("enemy")[i]);
                            }
                    }
            }
    }

    public void SkillHealing() {
        if(sp-costHeal>=0)
            {
                if(playerHealth.healthCount < playerHealth.maxHealth)
                    {
                        playerHealth.healthCount++;
                        //playerSP.spCount = sp - costHeal;
                        GameObject healthI = Instantiate(healthIndPrefab, new Vector2(healthInd.transform.GetChild(healthInd.transform.childCount - 1).position.x, healthInd.transform.GetChild(healthInd.transform.childCount - 1).position.y),  Quaternion.identity);
                        healthI.transform.SetParent(healthInd.transform);
                        healthI.transform.localPosition = new Vector2(healthI.transform.localPosition.x+50, healthI.transform.localPosition.y);
                        healthI.transform.localScale = new Vector3(1,1,1);
                    }
            }
    }
}
