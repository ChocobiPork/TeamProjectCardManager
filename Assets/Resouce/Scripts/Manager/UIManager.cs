using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TMP_InputField levelInput;

    #region 싱글톤
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void NextTurnBtnDown() // 다음 턴으로
    {
        TurnManager.instance.NextTurn(); // 턴 매니저 인스턴스에서 받아오기
    }

    /// <summary>
    /// 카드 오픈 버튼
    /// </summary>
    public void CardOpenBtn()
    {
        if (CardManager.instance.isOpen == false)
        {
            CardManager.instance.CardOpen();
        }
        else
        {
            Debug.Log("이미 오픈 상태임");
        }
    }


    /// <summary>
    /// 카드 클로즈 버튼
    /// </summary>
    public void CardCloseBtn()
    {
        if (CardManager.instance.isOpen == true)
        {
            CardManager.instance.CardClose();
        }
        else
        {
            Debug.Log("이미 클로즈 상태임");
        }
    }

    /// <summary>
    /// 인풋필드에 있는 레벨을 감지하고 레벨을 올려주는 버튼
    /// </summary>
    public void LevelUpBtn() //인풋필드에 있는 int값을 감지
    {
        if (levelInput.text == null) //인풋 필드가 비어있으면
        {
            Debug.Log("대머리"); //비어있음을 뜻함
        }
        else
        {
            int level = int.Parse(levelInput.text); // 인풋필드 형 변환
            LevelManager.instance.LevelUP(level); // 레벨 매니저의 인스턴스 가져오기
            //CardManager.instance.CardTargetLevel(); //카드 매니저의 인스턴스를 가져와 카드를 오픈할 레벨이 되는지 확인
        }
    }
}   
