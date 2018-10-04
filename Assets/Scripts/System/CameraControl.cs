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
}
