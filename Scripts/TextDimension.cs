using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDimension : MonoBehaviour
{
    public RectTransform rectParent;
    public RectTransform abc;
    void Update()
    {
        abc.sizeDelta = new Vector2(rectParent.rect.height, rectParent.rect.height);
    }
}
