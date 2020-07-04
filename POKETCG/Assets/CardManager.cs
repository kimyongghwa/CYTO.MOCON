using System.Collections;
using UnityEngine;
using SocketIO;
//using System;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
    public GameObject[] pc = new GameObject[3];
    public GameObject[] skill = new GameObject[3];
    public GameObject gg;
    public GameObject de;
    GameObject canvas;
    public GameObject battleScene;
    public bool isAi;
    public bool isMulti;
    public bool isMultiEneme;
    public int[] haveCard = new int[4];
    public AISkill ai;
    public int[] CardNum = new int[5];
    public Animator[] animator = new Animator[4];

    private SocketIOComponent socket;

    Dictionary<string, string> testdata = new Dictionary<string, string>();

    public void StartServer()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        socket.On("open", Open);

        socket.On("boop", (SocketIOEvent e) => {    
            Debug.Log(string.Format("[name: {0}, testdata: {1}]", e.name, e.data));
            testdata["email"] = "email.com";
            testdata["pass"] = "1234";
            socket.Emit("user:login", new JSONObject(testdata));
            Debug.Log(string.Format("[name: {0}, testdata: {1}]", e.name, e.data));
        });

        socket.On("receive", (SocketIOEvent e) => {
            Debug.Log(string.Format("[name: {0}, testdata: {1}]", e.name, e.data));
            testdata["email"] = "aa@email.com";
            testdata["pass"] = "aa";
            //socket.Emit("user:login", new JSONObject(testdata));
            Debug.Log(string.Format("[name: {0}, testdata: {1}]", e.name, e.data));
        });

        socket.On("error", Error);
        socket.On("close", Close);

        StartCoroutine("BeepBoop");
    }

    private IEnumerator BeepBoop()
    {
        // wait 1 seconds and continue
        yield return new WaitForSeconds(1);

        socket.Emit("beep");

        // wait 3 seconds and continue
        yield return new WaitForSeconds(3);

        socket.Emit("beep");

        // wait 2 seconds and continue
        yield return new WaitForSeconds(2);

        socket.Emit("beep");

        // wait ONE FRAME and continue
        yield return null;

        socket.Emit("beep");
        socket.Emit("beep");
    }

    public void Open(SocketIOEvent e)
    {
        Debug.Log("SocketIO Open received: " + e.name + " " + e.data);
    }


    public void Charecter(SocketIOEvent e)
    {
        testdata["email"] = "email.com";
        testdata["pass"] = "1234";
        socket.Emit("user:login", new JSONObject(testdata));
        Debug.Log(string.Format("[name: {0}, testdata: {1}]", e.name, e.data));

    }

    public void Card(SocketIOEvent e)
    {
        testdata["email"] = "email.com";
        testdata["pass"] = "1234";
        socket.Emit("user:login", new JSONObject(testdata));
        Debug.Log(string.Format("[name: {0}, testdata: {1}]", e.name, e.data));

    }

    public void Skill(SocketIOEvent e)
    {
        testdata["email"] = "email.com";
        testdata["pass"] = "1234";
        socket.Emit("user:login", new JSONObject(testdata));
        Debug.Log(string.Format("[name: {0}, testdata: {1}]", e.name, e.data));

    }




    public void Error(SocketIOEvent e)
    {
        Debug.Log("SocketIO Error received: " + e.name + " " + e.data);
    }

    public void Close(SocketIOEvent e)
    {
        Debug.Log("SocketIO Close received: " + e.name + " " + e.data);
    }


private void Awake()
    {
        if (!isAi && !isMultiEneme)
        {
            canvas = GameObject.Find("Canvas");
            Instantiate(skill[PlayerPrefs.GetInt("PC", 1)], canvas.transform);
            GameObject a = Instantiate(pc[PlayerPrefs.GetInt("PC", 1)], battleScene.transform);
        }
        if (isMulti) // 멀티일 경우 상대 플레이어의 스킬과 캐릭터를 instantiate 한다.
        {
            Dictionary<string, string> a = new Dictionary<string, string>();
            socket.Emit("Character", new JSONObject(a));
        }
    }
    private void Start()
    {
        Reroll();
    }
    public void Reroll() // 카드를 교체한다.
    {
        if (isMultiEneme) // 서버에서 상대카드정보를 불러와야함
        {
            Dictionary<string, string>b = new Dictionary<string, string>();
            socket.Emit("Card");
        }
        else {
            for (int i = 1; i < 5; i++)
            {
                CardNum[i] = 0;
            }
            for (int i = 0; i < 4; i++)
            {
                animator[i].SetInteger("CardType", 0);
                haveCard[i] = Random.Range(1, 5);
                CardNum[haveCard[i]]++;
                StartCoroutine("RerollCoroutine", i);
            }
            if (isMulti) // 멀티인데 내 캐릭터일 경우 정보를 서버로 보냄
            {
                Dictionary<string, string> MyCharacter = new Dictionary<string, string>();
                socket.Emit("Character");
            }
            if (isAi) { // ai일 경우 나온 카드에 따라 사용할 기술을 정해준다.
                for (int i = ai.cards.Length - 1; i >= 0; i--)
                {
                    AiSelect(i);
                }
            }
        }
    }
    public void AiSelect(int i)
    {
        for (int j = 1; j < 5; j++)
        {
            if (ai.cards[i].cost[j] > CardNum[j])
                return;

        }
        BattleManager.Instance.EnemeCard = ai.cards[i];
    }


   public void Click()
    {
        if(!isMulti)
        {
            DealGyo();
            Reroll();
        }
        else
        {

        }
    }
   public void DealGyo() //딜교환부분
    {
        if(BattleManager.Instance.Card != null)
        {
            BattleManager.Instance.myInfo.guard += BattleManager.Instance.Card.getArmor;
            BattleManager.Instance.myInfo.nowHp += BattleManager.Instance.Card.hill;
        }
        if (BattleManager.Instance.EnemeCard != null)
        {
            BattleManager.Instance.otherInfo.guard += BattleManager.Instance.EnemeCard.getArmor;
            BattleManager.Instance.otherInfo.nowHp += BattleManager.Instance.EnemeCard.hill;
        }
        if (BattleManager.Instance.otherInfo.nowHp >= BattleManager.Instance.otherInfo.maxHp)
        {
            BattleManager.Instance.otherInfo.nowHp = BattleManager.Instance.otherInfo.maxHp;
        }
        if (BattleManager.Instance.myInfo.nowHp >= BattleManager.Instance.myInfo.maxHp)
        {
            BattleManager.Instance.myInfo.nowHp = BattleManager.Instance.myInfo.maxHp;
        }
        if (BattleManager.Instance.EnemeCard != null)
        {
            BattleManager.Instance.myInfo.guard -= BattleManager.Instance.EnemeCard.damage;
            if (BattleManager.Instance.myInfo.guard < 0)
            {
                BattleManager.Instance.myInfo.nowHp += BattleManager.Instance.myInfo.guard;
                BattleManager.Instance.myInfo.guard = 0;
            }
            if (BattleManager.Instance.EnemeCard.effect != null)
                Instantiate(BattleManager.Instance.EnemeCard.effect, BattleManager.Instance.myInfo.transform);
            if(BattleManager.Instance.EnemeCard.effectSelf != null)
                Instantiate(BattleManager.Instance.EnemeCard.effectSelf, BattleManager.Instance.otherInfo.transform);
            if(BattleManager.Instance.EnemeCard.damage != 0)
                StartCoroutine("PlayerHit", true);
        }
        if (BattleManager.Instance.Card != null)
        {
            BattleManager.Instance.otherInfo.guard -= BattleManager.Instance.Card.damage;
            if (BattleManager.Instance.otherInfo.guard < 0)
            {
                BattleManager.Instance.otherInfo.nowHp += BattleManager.Instance.otherInfo.guard;
                BattleManager.Instance.otherInfo.guard = 0;
            }
            if (BattleManager.Instance.Card.effect != null)
                Instantiate(BattleManager.Instance.Card.effect, BattleManager.Instance.otherInfo.transform);
            if (BattleManager.Instance.Card.effectSelf != null)
                Instantiate(BattleManager.Instance.Card.effectSelf, BattleManager.Instance.myInfo.transform);
            if (BattleManager.Instance.Card.damage != 0)
                StartCoroutine("PlayerHit", false);
        }
        BattleManager.Instance.Card = null;
        BattleManager.Instance.EnemeCard = null;
        if (BattleManager.Instance.otherInfo.nowHp <= 0)
            de.SetActive(true);
        if(BattleManager.Instance.myInfo.nowHp <= 0)
            gg.SetActive(true);
    }

    IEnumerator RerollCoroutine(int num)
    {
        yield return new WaitForSeconds(0.015f);
        animator[num].gameObject.transform.localRotation = Quaternion.Euler(0, 30, 0);
        yield return new WaitForSeconds(0.015f);
        animator[num].gameObject.transform.localRotation = Quaternion.Euler(0, 60, 0);
        yield return new WaitForSeconds(0.015f);
        animator[num].gameObject.transform.localRotation = Quaternion.Euler(0, 90, 0);
        yield return new WaitForSeconds(0.015f);
        animator[num].gameObject.transform.localRotation = Quaternion.Euler(0, 120, 0);
        yield return new WaitForSeconds(0.015f);
        animator[num].gameObject.transform.localRotation = Quaternion.Euler(0, 150, 0);
        yield return new WaitForSeconds(0.015f);
        animator[num].gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        animator[num].SetInteger("CardType", haveCard[num]);
    }

    IEnumerator PlayerHit(bool a)
    {
        GameObject p1 = BattleManager.Instance.otherInfo.gameObject;
        if (a)
            p1 = BattleManager.Instance.myInfo.gameObject;
        yield return new WaitForSeconds(0.03f);
        p1.transform.localRotation = Quaternion.Euler(0, 90, 0);
        yield return new WaitForSeconds(0.03f);
        p1.transform.localRotation = Quaternion.Euler(0, 180, 0);
        yield return new WaitForSeconds(0.03f);
        p1.transform.localRotation = Quaternion.Euler(0, 270, 0);
        yield return new WaitForSeconds(0.03f);
        p1.transform.localRotation = Quaternion.Euler(0, 360, 0);
    }  
}


