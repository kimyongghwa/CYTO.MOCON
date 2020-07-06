using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour
{
    public Image thisImage;
    public Color color;
    public bool enemeCard;
    CardManager cm;
    CardManager csm; // socket 처리
    public int[] cost = new int[5];
    public int damage;
    public int getArmor;
    public int getMiss;
    public int hill;
    public bool armorBreak;
    public GameObject effect;
    public GameObject effectSelf;

    public void Click()
    {
        for (int i = 1; i < 5; i++)
        {
            if (cost[i] > cm.CardNum[i])
                return;
        }
        if (!enemeCard)
        {
            BattleManager.Instance.Card = this;
            if (cm.isMulti)
                csm.SendSkill(this.name);
        }
        else
            BattleManager.Instance.EnemeCard = this;

    }

    public void Awake()
    {
        if (!enemeCard)
            cm = GameObject.Find("Manager").GetComponent<CardManager>();
        else
            cm = GameObject.Find("EnemeCardManager").GetComponent<CardManager>();
        csm = GameObject.Find("Manager").GetComponent<CardManager>();
    }
    public void Update()
    {
        if (BattleManager.Instance.Card == this)
            thisImage.color = new Color(255, 0, 0, 255);
        else
            thisImage.color = color;

    }
}

