using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	void Desutroy() {
        Destroy(gameObject);
    }

    void Disabling() {
        gameObject.SetActive(false);
    }
}
