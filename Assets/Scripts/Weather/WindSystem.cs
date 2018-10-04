using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSystem : MonoBehaviour {
    public int maxMovingWind, maxStillWind;

    private List<Collider2D> objects = new List<Collider2D>();

    void Start() {
        maxMovingWind = 5000; //when falling
        maxStillWind = 3000; //when placed
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Contains("brick"))
            { 
		        objects.Add(other);
            }
    }
 
	void OnTriggerExit2D(Collider2D other) {
        if (other.tag.Contains("brick"))
            { 
		        objects.Remove(other);
            }
    }

	void FixedUpdate() {
		for(int i = 0; i < objects.Count; i++)
		    {
                if (objects[i].attachedRigidbody!=null)
                    { 
            			Rigidbody2D body = objects[i].attachedRigidbody;
                        int rand = Random.Range(0,2);
                        if (rand == 0)
                            {
                                if(!objects[i].GetComponent<BrickControl>().dropped)
                                    {
                                        var force = Random.Range(0, maxMovingWind);
                                        body.AddForce (Vector2.right*force*Time.deltaTime);
                                    } else
                                        {
                                            var force = Random.Range(0, maxStillWind);
                                            body.AddForce (Vector2.right*force*Time.deltaTime);
                                        }
                            } else
                                {
                                    if(!objects[i].GetComponent<BrickControl>().dropped)
                                        {
                                            var force = Random.Range(0, maxMovingWind);
                                            body.AddForce (Vector2.left*force*Time.deltaTime);
                                        } else
                                            {
                                                var force = Random.Range(0, maxStillWind);
                                                body.AddForce (Vector2.left*force*Time.deltaTime);
                                            }
                                }
                    }
            }
	}
}
