using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockBtn : MonoBehaviour
{
    public Image currentImage;
    public Sprite onSprite;
    public Sprite offSprite;

    public bool Look = false;

    public void BtnToggle()
    {
        Look = !Look;
        if (!Look)
        {
            currentImage.sprite = offSprite;
        }
        else
        {
            currentImage.sprite = onSprite;
        }
    }
}
