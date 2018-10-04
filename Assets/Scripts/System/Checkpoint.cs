using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    Transform[] bricks = null;

	public void CheckpointFreez() {
        for(int i=0;i<transform.childCount;i++)
            {
                bricks[i] = transform.GetChild(i);
                bricks[i].GetComponent<Rigidbody2D>().isKinematic = true;
            }
    }
}
