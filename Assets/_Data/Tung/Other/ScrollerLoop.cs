using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollerLoop : LinkMonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 0.1f;
    public bool isScroll = false;

    void Update()
    {
        if (!isScroll)
        {
            scrollRect.verticalNormalizedPosition = 1f;
            return;
        }
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition -= scrollSpeed * Time.deltaTime;

            if (scrollRect.verticalNormalizedPosition <= 0f)
            {
                scrollRect.verticalNormalizedPosition = 1f;
            }
        }
    }

    public void setIsScroll(bool isScroll)
    {
        this.isScroll = isScroll;
    }
}
