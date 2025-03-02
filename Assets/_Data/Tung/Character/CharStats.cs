using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : LinkMonoBehaviour
{
    [Header("CharStats")]
    //[SerializeField] protected string charName;
    //public string CharName => charName;

    [SerializeField] protected int maxHP = 3;
    public int MaxHP => maxHP;

    [SerializeField] protected int maxMP = 1;
    public int MaxMP => maxMP;

    public int currHP { get; set; }
    public int currMP { get; set; }
    //public int DMG { get; set; }

    //private bool isAlive = true; // Kiểm tra nhân vật còn sống hay không
    private Coroutine mpRegenCoroutine; // Lưu coroutine để có thể dừng

    [SerializeField] protected CharCtrl _charCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharCtrl();
        this.LoadCharStats();
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
        currMP = maxMP;
        //DMG = 10;
        StartMPRegen(); // Kiểm tra và bắt đầu hồi MP nếu cần
    }

    public virtual void AddHP(int addHP)
    {
        this.currHP += addHP;
        if (this.currHP > this.MaxHP) this.currHP = this.MaxHP;
    }

    public virtual void SubHP(int deductHP)
    {
        this.currHP -= deductHP;
        if (this.currHP <= 0) this.Dead();
    }

    public virtual void AddMP(int addMP)
    {
        this.currMP += addMP;
        if (this.currMP > this.MaxMP) this.currMP = this.MaxMP;

        // Nếu MP đạt max, dừng coroutine
        if (currMP == maxMP && mpRegenCoroutine != null)
        {
            StopCoroutine(mpRegenCoroutine);
            mpRegenCoroutine = null;
        }
    }

    public virtual void SubMP(int deductMP)
    {
        this.currMP -= deductMP;
        if (this.currMP < 0) this.currMP = 0;

        // Nếu MP giảm xuống dưới max, kiểm tra và khởi động lại coroutine
        StartMPRegen();
    }

    private void StartMPRegen()
    {
        if (mpRegenCoroutine == null && currMP < maxMP && !_charCtrl.CharState.GetIsDead())
        {
            mpRegenCoroutine = StartCoroutine(RestoreMPOverTime());
        }
    }

    // Hồi 1 MP mỗi giây
    private IEnumerator RestoreMPOverTime()
    {
        while (currMP < maxMP && !_charCtrl.CharState.GetIsDead()) // Kiểm tra nếu còn sống thì hồi MP
        {
            yield return new WaitForSeconds(2f);
            AddMP(1);
        }

        // Nếu đã hồi đủ MP, dừng coroutine
        mpRegenCoroutine = null;
    }

    //Hàm xử lý nhân vật chết
    private void Dead()
    {
        _charCtrl.CharState.SetIsDead(true); // Đánh dấu nhân vật đã chết

        if (mpRegenCoroutine != null)
        {
            StopCoroutine(mpRegenCoroutine); // Dừng hồi MP
            mpRegenCoroutine = null;
        }

        _charCtrl.CharState.ChangeAnimationState("Die");
    }
}
