using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGenerator : MonoBehaviour {
    public GameObject brickJ, brickL, brickLong, brickS, brickZ, brickSquare, brickT, nextBrick, previewBrick;
    public Transform spawnBrickPos, previewBrickPos, healthInd;
    public Vector3 normalScale = new Vector3(.8f, .8f, 1)/*, previewScale = new Vector3(.4f, .4f, 1)*/;

    bool gameStarted;
    UserInput brickController;
    GameSystem gameSys;
    
    void Start () {
        brickController = GetComponent<UserInput>();
        gameSys = GetComponent<GameSystem>();
        SpawnNextBrick();
	}

    /*void Update () {
        previewBrick.transform.position = previewBrickPos.position;
        if(gameSys.gameOver || gameSys.winOver)
            {
                previewBrick.SetActive(false);
            }
	}*/

    public void SpawnNextBrick () {
        if (!gameStarted) //Run this code when the game has just started
            {
                gameStarted = true;
                nextBrick = Instantiate(GetRandomBrick(), spawnBrickPos.position, Quaternion.identity);
                //nextBrick.transform.position = new Vector2(nextBrick.transform.position.x,nextBrick.transform.position.y);
                nextBrick.GetComponent<BrickControl>().gameSys = gameObject;
                nextBrick.GetComponent<BrickControl>().healthInd = healthInd;
                nextBrick.transform.localScale = normalScale;

                //----------Enable this block if you want to use preview, then configure the rest----------//
                /*previewBrick = Instantiate(GetRandomBrick(), previewBrickPos.position, Quaternion.identity);
                previewBrick.GetComponent<BrickControl>().gameSys = gameObject;
                previewBrick.GetComponent<BrickControl>().healthInd = healthInd;
                previewBrick.GetComponent<Rigidbody2D>().isKinematic = true;
                previewBrick.GetComponent<Collider2D>().enabled = false;
                previewBrick.transform.localScale = previewScale;*/

                brickController.testPos = nextBrick.transform;
            }
        else
            {
                nextBrick = Instantiate(GetRandomBrick(), spawnBrickPos.position, Quaternion.identity);
                //nextBrick.transform.position = new Vector2(nextBrick.transform.position.x,nextBrick.transform.position.y);
                nextBrick.GetComponent<BrickControl>().gameSys = gameObject;
                nextBrick.GetComponent<BrickControl>().healthInd = healthInd;
                nextBrick.transform.localScale = normalScale;

                /*previewBrick.transform.localPosition = spawnBrickPos.position;
                nextBrick = previewBrick;*/
                brickController.testPos = nextBrick.transform;
                /*nextBrick.transform.position = new Vector2(nextBrick.transform.position.x,nextBrick.transform.position.y);
                nextBrick.GetComponent<Rigidbody2D>().isKinematic = false;
                nextBrick.GetComponent<Collider2D>().enabled = true;
                nextBrick.transform.localScale = normalScale;*/
                
                //----------Enable this block if you want to use preview, then configure the rest----------//
                /*previewBrick = Instantiate(GetRandomBrick(), previewBrickPos.position, Quaternion.identity);
                previewBrick.GetComponent<BrickControl>().gameSys = gameObject;
                previewBrick.GetComponent<BrickControl>().healthInd = healthInd;
                previewBrick.GetComponent<Rigidbody2D>().isKinematic = true;
                previewBrick.GetComponent<Collider2D>().enabled = false;
                previewBrick.transform.localScale = previewScale;*/
            }
    }

    GameObject GetRandomBrick () {
        GameObject randomBrick = null;
        int randNum = Random.Range(1, 7);
        switch (randNum)
            {
                case 1 :
                    randomBrick = brickJ;
                    break;
                case 2 :
                    randomBrick = brickL;
                    break;
                case 3 :
                    randomBrick = brickLong;
                    break;
                case 4 :
                    randomBrick = brickS;
                    break;
                case 5 :
                    randomBrick = brickSquare;
                    break;
                case 6 :
                    randomBrick = brickT;
                    break;
                case 7 :
                    randomBrick = brickZ;
                    break;
            }
        return randomBrick;
    }
}
