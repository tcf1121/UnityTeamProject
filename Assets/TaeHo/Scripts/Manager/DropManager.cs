using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    // ¸ó½ºÅÍ°¡ Á×¾úÀ» ¶§ °ñµå¸¦ ¶³¾î¶ß¸®´Â Manager »ý¼º

    public int currentGold = 0; // ÇöÀç °ñµå

    public void OnEnable()
    {
        NormalZombie1.OnZombieDied += AddGold;
    }

    public void OnDisable()
    {
        NormalZombie1.OnZombieDied -= AddGold;
    }

    public void AddGold(int goldAmount)
    {
        currentGold += goldAmount;
        Debug.Log($"{goldAmount}°ñµå¸¦ È¹µæÇÏ¼Ì½À´Ï´Ù!");
        Debug.Log($"ÇöÀç °ñµå´Â {currentGold}ÀÔ´Ï´Ù.");
    }
}
