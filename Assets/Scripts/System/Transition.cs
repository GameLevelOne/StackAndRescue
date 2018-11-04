using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {
    public GameObject Screen1, Screen2;

    Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public void TransitionStart(GameObject Scr1) {
        Screen1 = Scr1;
    }

    public void TransitionEnd(GameObject Scr2) {
        Screen2 = Scr2;
        anim.SetTrigger("trans");
    }

    public void TransitionEndR(GameObject Scr2) {
        Screen2 = Scr2;
        anim.SetTrigger("transR");
    }

    public void Transitions() {
        Screen2.SetActive(true);
        Screen1.SetActive(false);
    }
}
