using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using System.Linq;

public enum CardRarity
{
    General,
    Legend
}

public class CardManager : MonoBehaviour
{
    //public static CardManager Instance;

    public GameObject CardGroup; // 카드 뭉탱이
    public Animator GroupAni; //카드의 애니메이션

    public bool showCard = false;//카드를 보여준 상태인지
    public bool isOpen = false; // 카드가 열린 상태인지

    //--신규로 작성중인 구간
    //public List<GameObject> cardGroup; // 등장한 카드들

    public float cardCloseAnimationDuration = 1.0f; //카드가 닫히고 그 후 기다릴 시간

    // 동적으로 생성된 카드 오브젝트들을 저장할 리스트
    [Header("생성된 카드들")]
    public List<GameObject> instantiatedCards = new List<GameObject>();
    // 카드 등급을 저장할 리스트
    [Header("카드 등급들")]
    public List<CardRarity> assignedRarities = new List<CardRarity>();

    [Header("희귀도 설정")]
    [Range(0f, 1f)] // 0 ~ 1 까지
    public float legendChance = 0.2f; // 전설이 뜰 확률 (기본값 20%)

    [Header("카드 프리팹")]
    public GameObject generalCardPrefab; //일반 카드의 프리팹
    public GameObject legendCardPrefab; //전설 카드의 프리팹

    [Header("카드가 생성될 위치")]
    public Transform[] spawnPoints; // 카드가 생성될 위치 (4개의 Transform 필요)

    #region 싱글톤
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
    #endregion

    private void Start()
    {
    }

    private void Update()
    {
        //CardTargetLevel();
    }

    // UIManager에서 "클릭" 이벤트 발생 시 호출될 함수
    public void CardRarityOpen()
    {
        isOpen = true;
        //foreach (GameObject card in instantiatedCards)
        //{
        //    Destroy(card);
        //}
        //instantiatedCards.Clear();
        //assignedRarities.Clear();


        // 4개의 위치에 카드 생성 및 등급 할당
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            CardRarity rarity;
            float randomPoint = Random.value;

            if (randomPoint < legendChance)
            {
                rarity = CardRarity.Legend;
            }
            else
            {
                rarity = CardRarity.General;
            }

            assignedRarities.Add(rarity);

            // 등급에 맞는 프리팹 선택
            GameObject cardPrefab = (rarity == CardRarity.General) ? generalCardPrefab : legendCardPrefab; //삼항연산자 문법

            // 해당 위치에 프리팹 인스턴스화 (생성)
            GameObject newCard = Instantiate(cardPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
            instantiatedCards.Add(newCard);

            Debug.Log($"카드 {i + 1} ({spawnPoints[i].name} 위치) 등급: {rarity}");
        }
    }

    // "CardClose" 버튼 클릭 시 호출될 함수
    public void ResetCardList()
    {
        isOpen = false;

        // 씬의 카드 오브젝트들을 숨기거나 제거하는 로직 추가 가능
        foreach (GameObject card in instantiatedCards)
        {
            Destroy(card);
        }

        instantiatedCards.Clear();
        assignedRarities.Clear();
        Debug.Log("카드가 제거되고 리스트가 초기화 되었습니다");
    }

    /// <summary>
    /// 클릭된 카드를 제외한 나머지 카드를 닫고, 클릭된 카드는 새 애니메이션을 적용하는 함수
    /// </summary>
    /// <param name="clickedCard">클릭된 카드 오브젝트</param>
    public void CloseOtherCards(GameObject clickedCard)
    {
        Debug.Log($"클릭된 카드: {clickedCard.name}");

        foreach (GameObject card in instantiatedCards)
        {
            Animator cardAnimator = card.GetComponent<Animator>();

            if (cardAnimator != null)
            {
                // 클릭된 카드와 현재 카드가 다르다면 (나머지 카드)
                if (card != clickedCard)
                {
                    // "CardClose" 트리거 실행
                    cardAnimator.SetTrigger("CardClose");
                    Debug.Log($"{card.name}에 Close 애니메이션 적용");
                }
                // 클릭된 카드와 현재 카드가 같다면 (클릭된 카드)
                else
                {
                    //// "CardSelect" 트리거 실행
                    //cardAnimator.SetTrigger("CardSelect");
                    //Debug.Log($"{card.name}에 Select 애니메이션 적용");
                }
            }
        }

        // 애니메이션이 끝날 때까지 기다린 후 리셋 함수 호출 (기존 코루틴 유지)
        StartCoroutine(WaitForAnimationsAndResetRoutine());
    }

    /// <summary>
    /// 애니메이션 재생 시간만큼 기다린 후 모든 카드를 리셋하는 코루틴
    /// </summary>
    IEnumerator WaitForAnimationsAndResetRoutine()
    {
        // 지정된 시간만큼 기다립니다.
        yield return new WaitForSeconds(cardCloseAnimationDuration);

        Debug.Log("애니메이션 대기 시간 종료. 카드 리셋 함수 호출.");
        ResetCardList();
    }


    //---------------------------

    //public void ShowCard() //카드를 보여주는 함수
    //{
    //    CardGroup.SetActive(true);
    //    showCard = true;
    //}

    //public void HideCard() //카드를 숨기는 함수
    //{
    //    CardGroup.SetActive(false);
    //    showCard = false;
    //    isOpen = false;
    //}

    /// <summary>
    /// 카드를 오픈하는 함수
    /// </summary>
    //public void CardOpen()
    //{
    //    if (showCard == false) //카드를 보여주고 있지 있다면
    //    {
    //        Debug.Log("현재 카드를 보여주고 있지 않기에 카드를 열수 없습니다.");
    //        return;
    //    }
    //    else
    //    {
    //        GroupAni.SetTrigger("CardOpen"); // 애니메이션 재생
    //        isOpen = true;
    //    }
    //}


    /// <summary>
    ///// 카드를 클로즈 하는 함수
    ///// </summary>
    //public void CardClose()
    //{
    //    if (showCard == false) //카드를 보여주고 있지 않다면
    //    {
    //        Debug.Log("현재 카드를 보여주고 있지 않기에 카드를 열수 없습니다");
    //        return;
    //    }
    //    else
    //    {
    //        GroupAni.SetTrigger("CardClose"); // 트리거 변경
    //        isOpen = false;
    //    }
    //}

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
