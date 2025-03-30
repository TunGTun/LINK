using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : LinkMonoBehaviour
{
    [SerializeField] protected CharCtrl _charCtrl;

    SpriteRenderer healthBar;
    public Sprite[] healthBarSprites;

    public float shakeStrength = 0.1f;
    public float shakeDuration = 0.2f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharCtrl();
        healthBar = GetComponent<SpriteRenderer>();
        StartCoroutine(WaitLoadScene());
    }

    IEnumerator WaitLoadScene()
    {
        yield return new WaitForSeconds(1f);
        this.ChangeHealthBarUI();
    }

    protected virtual void LoadCharCtrl()
    {
        if (_charCtrl != null) return;
        _charCtrl = GetComponentInParent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    public void ChangeHealthBarUI()
    {
        StartCoroutine(WaitHealthBar());
    }

    IEnumerator WaitHealthBar()
    {
        healthBar.sprite = healthBarSprites[_charCtrl.CharStats.currHP];
        LeanTween.scale(this.gameObject, new Vector3(0.2f, 0.2f, 0.2f), 0.5f);
        transform.DOShakePosition(shakeDuration, shakeStrength);
        yield return new WaitForSeconds(1);
        LeanTween.scale(this.gameObject, new Vector3(0.0001f, 0.0001f, 0.0001f), 0.5f);
    }
}
