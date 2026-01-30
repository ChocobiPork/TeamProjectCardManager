using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    //public static TurnManager Instance;

    public string currentTurnOwner; // 현재 턴의 주인
    public int currentTurnId; // 현재 턴 수
    //public int cardOpenTurn = 15; //카드를 오픈할 턴

    public TextMeshProUGUI TurnOnwerText; // 현재 턴의 주인을 텍스트로 받기
    public TextMeshProUGUI currentTurnText; //현재  턴 수를 텍스트로 받기

    public enum TurnOwner //턴의 주인을 Enum으로 받기
    {
        Player,
        Monster
    }

    //#region 싱글톤
    //private void Awake() //싱글톤
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    //#endregion

    private void Start()
    {
        currentTurnOwner = TurnOwner.Player.ToString(); // 형 변환
    }

    private void Update() //텍스트 UI와 동기화
    {
        TurnOnwerText.text = currentTurnOwner;
        currentTurnText.text = currentTurnId.ToString(); // 형 변환

        //TurnCountAndCardOpen();
    }

    public void NextTurn() //다음 턴으로 넘기기 -> 턴종료 시에 함수 불러오면 됨.
    {
        if (GameManager.Instance.CardMgr.showCard) //카드가 보여지고 있을때는 턴이 넘어가지 않도록
        {
            Debug.Log("카드가 보여지고 있을땐 턴이 넘어가지 않습니다.");
            return;
        }
        else
        {
            currentTurnId++; // 카운트

            if (currentTurnOwner == "Player")
            {
                currentTurnOwner = TurnOwner.Monster.ToString();
            }
            else
            {
                currentTurnOwner = TurnOwner.Player.ToString();
            }
        }
    }

    /// <summary>
    /// 턴 카운트 수에 맞춰 카드를 여는 함수
    /// </summary>
    //public void TurnCountAndCardOpen()
    //{
    //    if (currentTurnId == cardOpenTurn) //현재 턴이 카드를 오픈할 턴이 되었을떄
    //    {
    //        GameManager.Instance.CardMgr.CardOpen(); //카드 매니저 인스턴스에서 함수 받기
    //    }
    //}

    //구조
    // 1. 첫번째 턴의 주인은 플레이어
    // 2. 턴을 넘겼을때 카운트 후 턴의 주인이 바뀌게
    // 3. 
}
