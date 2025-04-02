using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharStats : LinkMonoBehaviour
{
    [Header("CharStats")]
    //[SerializeField] protected string charName;
    //public string CharName => charName;

    [SerializeField] protected int maxHP = 3;
    public int MaxHP => maxHP;

    //[SerializeField] protected int maxMP = 1;
    //public int MaxMP => maxMP;

    public int currHP { get; set; }
    //public int currMP { get; set; }
    //public int DMG { get; set; }

    //private bool isAlive = true; // Kiểm tra nhân vật còn sống hay không
    //private Coroutine mpRegenCoroutine; // Lưu coroutine để có thể dừng

    [SerializeField] protected CharCtrl _charCtrl;

    public float deadDuration = 2f; 

    public RectTransform deadPanelRect, pauseButtonRect;
    public float tweenDuration;
    public CanvasGroup canvasGroup;
    public GameObject deadMenu;

    public Vector2 checkPointPos;
    public GameObject respawnEffect;

    public HealthBarUI healthBarUI;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharCtrl();
        this.LoadCharStats();
        checkPointPos = _charCtrl.transform.position;
        //HPChangeEffect();
    }

    protected virtual void LoadCharCtrl()
    {
        if (_charCtrl != null) return;
        _charCtrl = GetComponentInParent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    protected virtual void LoadCharStats()
    {
        //charName = transform.parent.name;
        currHP = maxHP;
        //currMP = maxMP;
        //DMG = 10;
        //StartMPRegen(); // Kiểm tra và bắt đầu hồi MP nếu cần
    }



    public virtual void AddHP(int addHP)
    {
        this.currHP += addHP;
        if (this.currHP > this.MaxHP) this.currHP = this.MaxHP;
        this.healthBarUI.ChangeHealthBarUI();
    }

    public virtual void SubHP(int deductHP)
    {
        this.currHP -= deductHP;
        if (this.currHP <= 0)
        {
            currHP = 0;
            this.Dead();
        }
        this.healthBarUI.ChangeHealthBarUI();
    }

    //public virtual void AddMP(int addMP)
    //{
    //    this.currMP += addMP;
    //    if (this.currMP > this.MaxMP) this.currMP = this.MaxMP;

    //    // Nếu MP đạt max, dừng coroutine
    //    if (currMP == maxMP && mpRegenCoroutine != null)
    //    {
    //        StopCoroutine(mpRegenCoroutine);
    //        mpRegenCoroutine = null;
    //    }
    //}

    //public virtual void SubMP(int deductMP)
    //{
    //    this.currMP -= deductMP;
    //    if (this.currMP < 0) this.currMP = 0;

    //    // Nếu MP giảm xuống dưới max, kiểm tra và khởi động lại coroutine
    //    StartMPRegen();
    //}

    //public void HPChangeEffect()
    //{
    //    healthBarOJ.SetActive(true);
    //    healthBar.sprite = healthBarSprites[currHP];
    //    StartCoroutine(HealthBarDuration());
    //}

    //private IEnumerator HealthBarDuration()
    //{
    //    yield return new WaitForSeconds(2f);
    //    healthBarOJ.SetActive(false);
    //}

    //private void StartMPRegen()
    //{
    //    if (mpRegenCoroutine == null && currMP < maxMP && !_charCtrl.CharState.GetIsDead())
    //    {
    //        mpRegenCoroutine = StartCoroutine(RestoreMPOverTime());
    //    }
    //}

    // Hồi 1 MP mỗi giây
    //private IEnumerator RestoreMPOverTime()
    //{
    //    while (currMP < maxMP && !_charCtrl.CharState.GetIsDead()) // Kiểm tra nếu còn sống thì hồi MP
    //    {
    //        yield return new WaitForSeconds(2f);
    //        AddMP(1);
    //    }

    //    // Nếu đã hồi đủ MP, dừng coroutine
    //    mpRegenCoroutine = null;
    //}

    //Hàm xử lý nhân vật chết
    private void Dead()
    {
        _charCtrl.CharState.SetIsDead(true); // Đánh dấu nhân vật đã chết

        //if (mpRegenCoroutine != null)
        //{
        //    StopCoroutine(mpRegenCoroutine); // Dừng hồi MP
        //    mpRegenCoroutine = null;
        //}

        _charCtrl.CharState.ChangeAnimationState("Die");
        StartCoroutine(StopTime());
    }

    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(deadDuration);
        deadMenu.SetActive(true);
        Time.timeScale = 0;
        DeadPanelIntro();
    }

    public void Respawn()
    {
        _charCtrl.CharState.SetIsDead(false);
        this.currHP = maxHP;
        _charCtrl.CharState.IsInvisible = true;
        Time.timeScale = 1;
        _charCtrl.Rigidbody2D.velocity = new Vector2(0, 0);
        _charCtrl.transform.localScale = Vector3.zero;
        _charCtrl.Rigidbody2D.simulated = false;
        LeanTween.sequence()
            .append(LeanTween.scale(respawnEffect, new Vector3(2600f, 2600f, 2600f), 1f))
            .append(LeanTween.scale(respawnEffect, new Vector3(0.0001f, 0.0001f, 0.0001f), 1f));
        StartCoroutine(WaitRespawnCoroutine());
    }

    IEnumerator WaitRespawnCoroutine()
    {
        yield return new WaitForSeconds(1f);
        DeadPanelOutro();
        deadMenu.SetActive(false);
        _charCtrl.transform.position = new Vector3(checkPointPos.x, checkPointPos.y, _charCtrl.transform.position.z);
        yield return new WaitForSeconds(1f);
        this.healthBarUI.ChangeHealthBarUI();
        _charCtrl.CharState.ChangeAnimationState("Idle");
        _charCtrl.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        _charCtrl.Rigidbody2D.simulated = true;
        _charCtrl.CharState.IsInvisible = false;
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkPointPos = pos;
    }

    void DeadPanelIntro()
    {
        canvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        deadPanelRect.DOScale(Vector3.one, tweenDuration).SetUpdate(true);
        pauseButtonRect.DOAnchorPosX(65, tweenDuration).SetUpdate(true);
    }

    void DeadPanelOutro()
    {
        canvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
        deadPanelRect.DOScale(Vector3.zero, tweenDuration).SetUpdate(true);
        pauseButtonRect.DOAnchorPosX(-65, tweenDuration).SetUpdate(true);
    }
}
