using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSystem : MonoBehaviour {
    public PhysicsMaterial2D physMat, physMatRain;

    private List<Collider2D> objects = new List<Collider2D>();
    
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

    private void OnDisable() {
		for(int i = 0; i < objects.Count; i++)
		    {
                if (objects[i] != null)
                    { 
			            Rigidbody2D body = objects[i].attachedRigidbody;
                        body.sharedMaterial = physMat;
                    }
            }
    }

    void FixedUpdate() {
		for(int i = 0; i < objects.Count; i++)
		    {
                if (objects[i] != null)
                    { 
			            Rigidbody2D body = objects[i].attachedRigidbody;
                        body.sharedMaterial = physMatRain;
                    }
            }
	}
}
