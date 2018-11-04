using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public float speed = 2f;
    public GameSystem gameSys;
    public float currHeightPos;

    void Update () {
        if(gameSys.lastBrick)
            { 
                currHeightPos = gameSys.lastBrick.position.y;
                if(currHeightPos>transform.position.y)
                    { 
                        Vector2 dir = new Vector3(transform.position.x, currHeightPos, transform.position.z);
                        transform.Translate(dir.normalized * speed * Time.deltaTime);
                    } else
                        {
                            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                        }
            }
    }

    public void ShakeCam(float time,bool boss) {
        StartCoroutine(Shaking(time,boss));
    }

    IEnumerator Shaking(float duration, bool boss) {
        float endTime = Time.time + duration;
        while(Time.time < endTime)
            {
                float ran;
                if(boss)
                    {
                        ran = Random.Range(-.07f, .07f);
                    } else
                        {
                            ran = Random.Range(-.03f, .03f);
                        }
                transform.position = new Vector3(transform.position.x + ran, transform.position.y, transform.position.z);
                duration -= Time.deltaTime;
                yield return null;
            }
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }
}
