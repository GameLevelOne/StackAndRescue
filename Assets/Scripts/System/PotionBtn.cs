using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBtn : MonoBehaviour {
    private Animator anim;
    public bool unfolded;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    public void OpenPotBar() {
        if(!unfolded)
            {
                anim.SetBool("fold",false);
                unfolded = true;
            } else
                {
                    anim.SetBool("fold",true);
                    unfolded = false;
                }
    }
}
