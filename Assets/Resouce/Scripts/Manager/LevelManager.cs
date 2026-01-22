using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int mPLV = 99; //플레이어 레벨의 최대값 (MaxPlayerLevel)
    public int currentPlayerLevel; //현재 플레이어의 레벨
    public TextMeshProUGUI currentLevel; //현재 레벨 표시
    public int[] targetLevels = { 15, 30, 33 };

    #region 싱글톤
    private void Awake() //싱글톤
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance);
        }
    }
    #endregion

    private void Update()
    {
        currentLevel.text = currentPlayerLevel.ToString(); // 형변환
    }

    public void LevelUP(int _ILevel) // IncreaseLevel
    {
        if (CardManager.instance.showCard == true) //이미 카드가 보여지고 있다면 (짜피 카드가 열리는 동안엔 턴이든 뭐든 증가 못하게 막아놨으니 레벨업도 못하게 막기)
        {
            Debug.Log("카드가 열린 상태에서는 레벨업을 할 수 없습니다.");
            return; // 카드가 열려있으면 레벨업 불가
        }
        else
        { 
            if (currentPlayerLevel <= mPLV) // 플레이어 레벨이 99 미만일때
            {
                currentPlayerLevel += _ILevel; // 레벨값을 UIManager에서 지정한 매개변수 만큼 받아오기
            }
            else //넘으면
            {
                currentPlayerLevel = mPLV; // 그냥 레벨 제한 때려버리기
            }
        }
    }
}
