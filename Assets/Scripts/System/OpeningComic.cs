using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningComic : MonoBehaviour {
    public Animator anim;
    public GameObject storymenu;

	// Use this for initialization
	void Start () {
        anim = GetComponentInParent<Animator>();
	}

    void OnMouseDown() {
        anim.SetTrigger("skip");
    }

    void OnDisable() {
        storymenu.SetActive(true);
    }
}
