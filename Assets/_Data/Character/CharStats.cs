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

    //[SerializeField] protected int maxMP = 100;
    //public int MaxMP => maxMP;

    public int currHP { get; set; }

    //public int currMP { get; set; }

    //public int DMG { get; set; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharStats();
    }

    protected virtual void LoadCharStats()
    {
        //charName = transform.parent.name;
        currHP = maxHP;
        //currMP = maxMP;
        //DMG = 10;
    }

    public virtual void AddHP(int addHP)
    {
        this.currHP += addHP;
        if (this.currHP > this.MaxHP) this.currHP = this.MaxHP;
    }

    public virtual void SubHP(int deductHP)
    {
        this.currHP -= deductHP;
        if (this.currHP < 0) this.currHP = 0;
    }
}
