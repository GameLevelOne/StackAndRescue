using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectives : MonoBehaviour {
    public string goal;
    public bool modeStory, modeBuild, modeDefend, modeEndless, win, lose, kill;
    public int checkpointReached, enemyKilled;
    public int checkpointGoal, enemyGoal;
    public Text goalTxt,statusTxt;
    public Camera cam;
    public Transform foundation;
    
    void Start () {
		if(goal=="story")
            {
                modeStory = true; 
            } else if(goal=="build")
                {
                    modeBuild = true;
                } else if (goal=="defend")
                    {
                        modeDefend = true;
                    } else if (goal=="endless")
                        {
                            modeEndless = true; 
                        }
    }
	
	void Update () {
		if(modeStory)
            {
                StoryMode();
            } else if(modeBuild)
                {
                    BuildMode();
                } else if(modeDefend)
                    {
                        DefendMode();
                    } else if(modeEndless)
                        { 
                            EndlessMode(); 
                        }
    }
    
    void StoryMode() {
        if(kill)
            {
                goalTxt.text = enemyGoal.ToString();
                statusTxt.text = enemyKilled.ToString();
            } else
                {
                    goalTxt.text = checkpointGoal.ToString();
                    statusTxt.text = checkpointReached.ToString();
                }
        if(checkpointReached==checkpointGoal)
            {
                if(kill)
                    {
                        if(enemyKilled>=enemyGoal)
                            {
                                win = true;
                                StartCoroutine(ZoomOut());
                            } else
                                {
                                    lose = true;
                                }
                    } else
                        {
                            win = true;
                        }
                //StartCoroutine(ZoomOut());
            }
    }

    void BuildMode() {
        goalTxt.text = checkpointGoal.ToString();
        statusTxt.text = checkpointReached.ToString();
        if(checkpointReached==checkpointGoal)
            {
                win = true;
                //StartCoroutine(ZoomOut());
            }
    }

    void DefendMode() {
        goalTxt.text = enemyGoal.ToString();
        statusTxt.text = enemyKilled.ToString();
        if(enemyKilled==enemyGoal)
            {
                win = true;
                StartCoroutine(ZoomOut());
            }
    }

    void EndlessMode() {
        goalTxt.text = checkpointReached.ToString();
        statusTxt.text = enemyKilled.ToString();
    }

    IEnumerator ZoomOut() {
        while (!foundation.GetComponent<Renderer>().isVisible)
        {
            cam.orthographicSize += 1;
            yield return new WaitForSeconds(.5f);
        }
    }
}
