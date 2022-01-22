using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimations : MonoBehaviour
{
    public Image Title;
    //public RectTransform Panel;

    private void Start()
    {
        LeanTween.value(gameObject, a => Title.color = new Color(1, 1, 1, a), 0, 1, 2);

        //Panel.anchoredPosition = new Vector2(-Panel.rect.width, 0);
        //LeanTween.moveX(Panel, 0, 1);
    }
}
