using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int mPLV = 99; //플레이어 레벨의 최대값 (MaxPlayerLevel)
    public TextMeshProUGUI currentLevel; //현재 레벨 표시

    // 실제 플레이어의 레벨 데이터를 저장할 필드
    public int _currentPlayerLevel;

    // 프로퍼티를 사용하여 레벨 변경 감지 및 처리
    public int CurrentPlayerLevel
    {
        get { return _currentPlayerLevel; }
        private set
        {
            // 값이 실제로 변경될 때만 로직 실행
            if (_currentPlayerLevel != value)
            {
                int previousLevel = _currentPlayerLevel; //현재 레벨값을 저장 (old Level)
                _currentPlayerLevel = value; // 값 업데이트 (New Level)

                // 1. 레벨업 감지 시 실행할 함수 호출
                HandlePlayerLevelUp(previousLevel, _currentPlayerLevel);

                // 2. UI 업데이트
                if (currentLevel != null)
                {
                    currentLevel.text = _currentPlayerLevel.ToString();
                }
            }
        }
    }

    private void Start()
    {
        // 게임 시작 시 초기 레벨 설정 (이때도 set 프로퍼티 값 호출)
        CurrentPlayerLevel = 1;
    }

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

    // 어짜피 Set에서 받아오기때문에 굳이 업데이트에서 항시 수정할 필요가 없음.
    //private void Update()
    //{
    //    currentLevel.text = currentPlayerLevel.ToString(); // 형변환
    //}

    public void LevelUP(int _ILevel) // IncreaseLevel
    {
        //현재 플레이어에게 카드가 보여지고 있다면 레벨업 불가
        if (CardManager.instance.showCard == true)
        {
            Debug.Log("카드가 열린 상태에서는 레벨업을 할 수 없습니다.");
            return; //반환
        }
        else //카드가 열린 상태가 아님
        {
            int newLevel = CurrentPlayerLevel + _ILevel; //레벨값 올리기

            if (newLevel <= mPLV)
            {
                // 프로퍼티에 값을 할당하면 HandlePlayerLevelUp 함수가 자동으로 호출됨
                CurrentPlayerLevel = newLevel;
            }
            else
            {
                // 최대 레벨 제한
                CurrentPlayerLevel = mPLV;
            }
        }
    }

    /// <summary>
    /// 레벨 변경을 플레이어 레벨 프로퍼티로 받아 감지
    /// </summary>
    /// <param name="oldLevel">과거 값</param>
    /// <param name="newLevel">현재 값</param>
    public void HandlePlayerLevelUp(int oldLevel, int newLevel)
    {
        Debug.Log("레벨이 변경됨");
        CardManager.instance.ShowCard(); //레벨이 변경됨을 감지했으니 카드를 보여줌
    }
}