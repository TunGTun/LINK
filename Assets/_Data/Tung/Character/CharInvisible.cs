using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInvisible : LinkMonoBehaviour
{
    [Header("CharInvisible")]

    protected bool _canInvisible;

    public float invisibleDuration = 3f; //Tạm
    public float invisibleCooldown = 10f; // Thời gian chờ giữa các lần lướt
    private float _lastInvisibleTime; // Lưu thời gian lần lướt cuối cùng

    [SerializeField] protected CharCtrl _charCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharCtrl();
        _lastInvisibleTime = Time.time - invisibleCooldown;
    }

    protected virtual void LoadCharCtrl()
    {
        if (_charCtrl != null) return;
        _charCtrl = GetComponentInParent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    private void Update()
    {
        this.CheckInvisible();
    }

    private void FixedUpdate()
    {
        this.Invisible();
    }

    protected virtual void CheckInvisible()
    {
        if (InputManager.Instance.InvisibleInput && Time.time >= _lastInvisibleTime + invisibleCooldown
            //&& _charCtrl.CharStats.currMP >= this.dashMP
            ) this._canInvisible = true;
    }

    protected virtual void Invisible()
    {
        if (!this._canInvisible) return;

        if (_charCtrl.CharState.GetIsDead()) return;

        _charCtrl.CharState.IsInvisible = true;

        AudioManager.Instance.PlaySFX("Invisible");

        StartCoroutine(LerpColor(new Color(1f, 1f, 1f, 0.5f), 0.2f));
        Invoke("Appear", invisibleDuration);

        this._canInvisible = false;
        _lastInvisibleTime = Time.time;
    }

    protected virtual void Appear()
    {
        _charCtrl.CharState.IsInvisible = false;
        StartCoroutine(LerpColor(Color.white, 0.2f));
    }

    IEnumerator LerpColor(Color targetColor, float duration)
    {
        Color startColor = _charCtrl.SpriteRenderer.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            _charCtrl.SpriteRenderer.color = Color.Lerp(startColor, targetColor, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _charCtrl.SpriteRenderer.color = targetColor;
    }
}
