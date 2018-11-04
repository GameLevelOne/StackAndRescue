using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovPropUI : MonoBehaviour {
    public float defspeed;
    public bool toLeft, toRight;
    public int xHome, xTarget;

    float speed;
    Vector2 home,target,startLoc;
    Vector2 minCam,maxCam;
    Image sprite;
    
	void Start () {
        sprite = GetComponent<Image>();
        minCam = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxCam = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if(toLeft)
            {
                home = new Vector2(maxCam.x + xHome, transform.position.y);
                target = new Vector2(minCam.x - xTarget, transform.position.y);
            } else if (toRight)
                {
                    home = new Vector2(minCam.x - xHome, transform.position.y);
                    target = new Vector2(maxCam.x + xTarget, transform.position.y);
                }
        startLoc = new Vector2(home.x, home.y);
        speed = defspeed;
	}
	
	void Update () {
        minCam = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxCam = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (toLeft)
            {
                home = new Vector2(maxCam.x + xHome, startLoc.y);
                target = new Vector2(minCam.x - xTarget, transform.position.y);
            } else if (toRight)
                {
                    home = new Vector2(minCam.x - xHome, startLoc.y);
                    target = new Vector2(maxCam.x + xTarget, transform.position.y);
                }
        if (Vector2.Distance(transform.position, target) <= 0.2f)
            {
                sprite.enabled = false;
                speed = 0;
                int rand = Random.Range(-12, 3);
                float del = Random.Range(4,8);
                StartCoroutine(DelayRepeat(rand,del));
            } else
                {
                    sprite.enabled = true;
                    speed = defspeed;
                    Vector2 dir = new Vector2((target.x - transform.position.x), (target.y - transform.position.y));
                    transform.Translate(dir.normalized * speed * Time.deltaTime);
                }
    }

    IEnumerator DelayRepeat(int rand,float del) {
        yield return new WaitForSeconds(del);
        transform.position = new Vector2(home.x, home.y + rand);
        speed = 0;
    }
}
