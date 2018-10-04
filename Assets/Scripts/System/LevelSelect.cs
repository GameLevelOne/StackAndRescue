using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    public int lvl;

	public void SelecetLevel() {
        SceneManager.LoadScene(lvl);
    }
}
