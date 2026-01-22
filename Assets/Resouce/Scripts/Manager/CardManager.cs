using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using System.Linq;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    public GameObject CardGroup; // 카드 뭉탱이
    public Animator GroupAni; //카드의 애니메이션

    public bool showCard = false;//카드를 보여준 상태인지s
    public bool isOpen = false; // 카드가 열린 상태인지

    #region 싱글톤
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
    }

    private void Update()
    {
        CardTargetLevel();
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
    }

    /// <summary>
    /// 카드를 오픈하는 함수
    /// </summary>
    public void CardOpen()
    {
        isOpen = true;
        GroupAni.SetTrigger("CardOpen"); // 애니메이션 재생
    }


    /// <summary>
    /// 카드를 클로즈 하는 함수
    /// </summary>
    public void CardClose()
    {
        isOpen = false;
        GroupAni.SetTrigger("CardClose"); // 애니메이션 재생
    }

    /// <summary>
    /// //현재 레벨이 카드를 오픈하기 위한 레벨과 맞다면 카드를 오픈하는 함수
    /// </summary>
    //public void CardTargetLevel()
    //{
    //    if (LevelManager.instance.targetLevels.Contains(GameManager.Instance.playerLevel)) //레벨값이 레벨 매니저에서 원하는 레벨에 맞는다면
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

    /// <summary>
    /// 현재 플레이어의 레벨이 카드를 오픈하기 위한 레벨과 맞다면 카드를 오픈하는 함수
    /// </summary>
    public void CardTargetLevel()
    {
        if (LevelManager.instance.targetLevels.Contains(LevelManager.instance.currentPlayerLevel))
        {
            ShowCard();
        }
    }
}
