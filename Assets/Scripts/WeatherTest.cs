using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherTest : MonoBehaviour {
    public GameObject windArea, rainArea;

    bool windy, rainy;

    public void WindActivate() {
        if (!windy)
            {
                windArea.SetActive(true);
                windy = true;
            } else
                {
                    windArea.SetActive(false);
                    windy = false;
                }
    } 
    
    public void RainActivate() {
        if (!rainy)
            {
                rainArea.SetActive(true);
                rainy = true;
            } else
                {
                    rainArea.SetActive(false);
                    rainy = false;
                }
    } 
}