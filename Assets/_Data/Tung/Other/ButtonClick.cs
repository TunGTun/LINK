using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : LinkMonoBehaviour
{
    protected override void Start()
    {
        base.Start();
        Button[] allButtons = FindObjectsOfType<Button>(true);

        foreach (Button btn in allButtons)
        {
            btn.onClick.AddListener(() => AudioManager.Instance.PlaySFX("Click"));
        }
    }
}
