using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovProp : MonoBehaviour {
    public float defspeed;
    public bool toLeft, toRight;
    public int xHome, xTarget;

    float speed;
    Vector2 home,target;
    Vector2 minCam,maxCam;
    SpriteRenderer sprite;
    
	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
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
        speed = defspeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(transform.position, target) <= 0.2f)
            {
                sprite.enabled = false;
                speed = 0;
                int rand = Random.Range(-12, 3);
                float del = Random.Range(7,14);
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
        if(toLeft)
            {
                target = new Vector2(minCam.x - xTarget, transform.position.y);
            } else if (toRight)
                {
                    target = new Vector2(maxCam.x + xTarget, transform.position.y);
                }
        speed = 0;
    }
}
