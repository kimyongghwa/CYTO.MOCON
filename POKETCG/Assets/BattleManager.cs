using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public PlayerInfo myInfo;
    public PlayerInfo otherInfo;
    public CardInfo Card;
    public CardInfo EnemeCard;
    static BattleManager instance;
    public static BattleManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            instance = this;
        }

    }
    private void Start()
    {
    }
}
