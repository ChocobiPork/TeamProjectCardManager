using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using System.Linq;

public class CardManager : MonoBehaviour
{
    //public static CardManager Instance;

    public GameObject CardGroup; // 카드 뭉탱이
    public Animator GroupAni; //카드의 애니메이션

    public bool showCard = false;//카드를 보여준 상태인지s
    public bool isOpen = false; // 카드가 열린 상태인지

    //#region 싱글톤
    //private void Awake()
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
    }

    private void Update()
    {
        //CardTargetLevel();
    }

    public void ShowCard() //카드를 보여주는 함수
    {
        CardGroup.SetActive(true);
        showCard = true;
    }

    public void HideCard() //카드를 숨기는 함수
    {
        CardGroup.SetActive(false);
        showCard = false;
        isOpen = false;
    }

    public void CardInfo() //카드의 정보를 저장하는 함수 -> 예 : 현재 카드의 속성,이름,정보
    {

    }

    /// <summary>
    /// 카드를 오픈하는 함수
    /// </summary>
    public void CardOpen()
    {
        if (showCard == false) //카드를 보여주고 있지 있다면
        {
            Debug.Log("현재 카드를 보여주고 있지 않기에 카드를 열수 없습니다.");
            return;
        }
        else
        {
            GroupAni.SetTrigger("CardOpen"); // 애니메이션 재생
            isOpen = true;
        }
    }


    /// <summary>
    /// 카드를 클로즈 하는 함수
    /// </summary>
    public void CardClose()
    {
        if (showCard == false) //카드를 보여주고 있지 않다면
        {
            Debug.Log("현재 카드를 보여주고 있지 않기에 카드를 열수 없습니다");
            return;
        }
        else
        {
            GroupAni.SetTrigger("CardClose"); // 트리거 변경
            isOpen = false;
        }
    }

    public void onCardClick()
    { 
        
    }

    #region 숨김 항목 - 버린 함수들
    /// <summary>
    /// //현재 레벨이 카드를 오픈하기 위한 레벨과 맞다면 카드를 오픈하는 함수
    /// </summary>
    //public void CardTargetLevel()
    //{
    //    if (GameManager.Instance.LevelMgr.targetLevels.Contains(GameManager.Instance.playerLevel)) //레벨값이 레벨 매니저에서 원하는 레벨에 맞는다면
    //    {
    //        if (isOpen == false) //카드가 오픈상태 인가 까지 계산
    //        {
    //            CardOpen(); //카드 열기
    //        }
    //        else
    //        {
    //            Debug.Log("ㄴㄴ 이미 열림");
    //        }
    //    }
    //}

    //타겟 레벨 삭제로 필요 없어짐

    /// <summary>
    /// 현재 플레이어의 레벨이 카드를 오픈하기 위한 레벨과 맞다면 카드를 오픈하는 함수
    /// </summary>
    //public void CardTargetLevel()
    //{
    //    if (GameManager.Instance.LevelMgr.targetLevels.Contains(GameManager.Instance.LevelMgr.currentPlayerLevel))
    //    {
    //        ShowCard();
    //    }
    //}
    #endregion
}
