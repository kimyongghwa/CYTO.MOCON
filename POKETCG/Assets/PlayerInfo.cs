using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public bool pc;
    public int maxHp;
    public int nowHp;
    public int guard;
    public Image HpImage;
    public Text HpText;
    public Text GuardText;

    private void Awake()
    {
    }
    private void Update()
    {
        if (pc)
            BattleManager.Instance.myInfo = this.GetComponent<PlayerInfo>();
        HpImage.fillAmount = (float)nowHp / maxHp;
        HpText.text = "(" + nowHp + " / " + maxHp + ')';
        GuardText.text = guard+"";
    }
}
