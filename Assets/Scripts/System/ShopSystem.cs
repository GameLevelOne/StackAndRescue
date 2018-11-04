using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour {
    public Text accCoinsTxt;
    public int accCoins,heroSkinsNum,princessSkinsNum,brickThemesNum,currPrice;

    void Awake() {
        if(!PlayerPrefs.HasKey("coinsSaved"))
            {
                PlayerPrefs.SetInt("coinsSaved",0);
            }

        #region ManaCharge Check Key
        if(!PlayerPrefs.HasKey("manaCharge1"))
            {
                PlayerPrefs.SetInt("manaCharge1",0);
            }
        if(!PlayerPrefs.HasKey("manaCharge2"))
            {
                PlayerPrefs.SetInt("manaCharge2",0);
            }
        if(!PlayerPrefs.HasKey("manaCharge3"))
            {
                PlayerPrefs.SetInt("manaCharge3",0);
            }
        #endregion

        #region HeroSkin Check Key
        //0=not bought, 1=bought
        #region Past Method Try
        /*if(!PlayerPrefs.HasKey("heroSkin1"))
            {
                PlayerPrefs.SetInt("heroSkin1",0); 
            }
        if(!PlayerPrefs.HasKey("heroSkin2"))
            {
                PlayerPrefs.SetInt("heroSkin2",0);
            }
        if(!PlayerPrefs.HasKey("heroSkin3"))
            {
                PlayerPrefs.SetInt("heroSkin3",0);
            }
        if(!PlayerPrefs.HasKey("heroSkin4"))
            {
                PlayerPrefs.SetInt("heroSkin4",0);
            }
        if(!PlayerPrefs.HasKey("heroSkin5"))
            {
                PlayerPrefs.SetInt("heroSkin5",0);
            }
        if(!PlayerPrefs.HasKey("heroSkin6"))
            {
                PlayerPrefs.SetInt("heroSkin6",0);
            }*/
        #endregion
        for(int i=0;i<heroSkinsNum;i++)
            {
                if(!PlayerPrefs.HasKey("heroSkin"+(i+1)))
                    {
                        PlayerPrefs.SetInt("heroSkin"+(i+1),0);
                    }
            }
        #endregion

        #region PrincessSkin Check Key
        //0=not bought, 1=bought
        for (int i=0;i<princessSkinsNum;i++)
            {
                if(!PlayerPrefs.HasKey("princessSkin"+(i+1)))
                    {
                        PlayerPrefs.SetInt("princessSkin"+(i+1),0);
                    }
            }
        #endregion

        #region BrickTheme Check Key
        //0=not bought, 1=bought
        for(int i=0;i<brickThemesNum;i++)
            {
                if(!PlayerPrefs.HasKey("brickTheme"+(i+1)))
                    {
                        PlayerPrefs.SetInt("brickTheme"+(i+1),0);
                    }
            }
        #endregion
    }

    void Update() {
        CheckCoins();
        for(int i=0;i<6;i++)
            {
                Debug.Log("Brick Theme "+(i+1)+" : "+PlayerPrefs.GetInt("brickTheme"+(i+1)));
            }
        for(int i=0;i<6;i++)
            {
                Debug.Log("Hero Skin "+(i+1)+" : "+PlayerPrefs.GetInt("heroSkin"+(i+1)));
            }
        for(int i=0;i<6;i++)
            {
                Debug.Log("Princess Skin "+(i+1)+" : "+PlayerPrefs.GetInt("princessSkin"+(i+1)));
            }
        for(int i=0;i<3;i++)
            {
                Debug.Log("Magic Potion "+(i+1)+" : "+PlayerPrefs.GetInt("manaCharge"+(i+1)));
            }
    }

    public void CheckCoins() {
        accCoins = PlayerPrefs.GetInt("coinsSaved");
        accCoinsTxt.text = accCoins.ToString();
    }

    public void Price(int price) {
        currPrice = price;
    }

    public void BuyItemSkin(string skinName) {
        if((accCoins-currPrice)>=0)
            {
                int leftOver = accCoins-currPrice;
                PlayerPrefs.SetInt("coinsSaved",leftOver);

                PlayerPrefs.SetInt(skinName,1);
            }
    }

    public void BuyManaPotion(string potionType) {
        if((accCoins-currPrice)>=0)
            {
                int leftOver = accCoins-currPrice;
                PlayerPrefs.SetInt("coinsSaved",leftOver);

                int currPotNum = PlayerPrefs.GetInt(potionType);
                PlayerPrefs.SetInt(potionType,(currPotNum+1));
            }
    }
}
