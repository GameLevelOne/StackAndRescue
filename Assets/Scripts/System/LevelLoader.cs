using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
    //Level & Mode Lock Manager
    public GameObject loadingScreen;
    public Slider slider;
    public Button[] lvlBtns;

    int levelReached;

    private void Start() {
        if(!PlayerPrefs.HasKey("levelReached"))
            {
                PlayerPrefs.SetInt("levelReached", 1);
            }

        levelReached = PlayerPrefs.GetInt("levelReached");

        for(int i=0;i<lvlBtns.Length;i++)
            {
                if(i+1>levelReached)
                    {
                        lvlBtns[i].interactable = false;
                    } else
                        {
                            lvlBtns[i].interactable = true;
                        }
            }
    }

    private void Update() {
        CheckStoryProgress();
        Debug.Log(PlayerPrefs.GetInt("levelReached"));
    }

    public void CheckStoryProgress() {
        levelReached = PlayerPrefs.GetInt("levelReached");

        for(int i=0;i<lvlBtns.Length;i++)
            {
                if(i+1>levelReached)
                    {
                        lvlBtns[i].interactable = false;
                    } else
                        {
                            lvlBtns[i].interactable = true;
                        }
            }
    }

    public void LoadLevel(int lvlIndex) {
        StartCoroutine(LoadAsync(lvlIndex));
    }
	
	IEnumerator LoadAsync(int lvlIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(lvlIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress/.9f);
                slider.value = progress;
                yield return null;
            }
    }
}
