using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public bool isOpen = false;
    public float cardCloseAnimationDuration = 1.0f;

    [Header("생성된 카드들 관리")]
    private List<GameObject> instantiatedCards = new List<GameObject>();

    [Header("희귀도 확률 설정")]
    [Range(0f, 1f)]
    public float legendChance = 0.2f;

    [Header("등급별 카드 풀")]
    public List<GameObject> generalCardPool;
    public List<GameObject> legendCardPool;

    [Header("카드가 생성될 위치")]
    public Transform[] spawnPoints;

    [Header("플레이어가 획득한 카드 이름 리스트")]
    public List<string> selectCardNames = new List<string>();

    public void CardRarityOpen()
    {
        if (isOpen) return;
        isOpen = true;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Card.CardRarity rarity = (Random.value < legendChance) ? Card.CardRarity.Legend : Card.CardRarity.General;
            GameObject selectedPrefab = GetRandomCardFromPool(rarity);

            if (selectedPrefab != null)
            {
                GameObject newCard = Instantiate(selectedPrefab, spawnPoints[i].position, spawnPoints[i].rotation); 
                instantiatedCards.Add(newCard);

                Card cardScript = newCard.GetComponent<Card>();
                if (cardScript != null) cardScript.rarity = rarity;
            }
        }
    }

    private GameObject GetRandomCardFromPool(Card.CardRarity rarity)
    {
        if (rarity == Card.CardRarity.Legend)
        {
            return (legendCardPool.Count > 0) ? legendCardPool[Random.Range(0, legendCardPool.Count)] : null; //삼항 연산자
        }
        else
        {
            return (generalCardPool.Count > 0) ? generalCardPool[Random.Range(0, generalCardPool.Count)] : null;
        }
    }

    public void AddSelectCard(GameObject clickedCardInstance)
    {
        // Instantiate 시 붙는 "(Clone)" 문구 제거하여 순수 이름만 추출
        string cleanName = clickedCardInstance.name.Replace("(Clone)", "").Trim();

        selectCardNames.Add(cleanName);
        Debug.Log($"플레이어 카드 리스트에 이름 저장 완료: {cleanName}");
    }

    public void CloseOtherCards(GameObject clickedCard)
    {
        foreach (GameObject card in instantiatedCards)
        {
            if (card == null) continue;
            Animator ani = card.GetComponent<Animator>();
            if (ani != null && card != clickedCard) ani.SetTrigger("CardClose");
        }
        StartCoroutine(WaitForAnimationsAndResetRoutine());
    }

    IEnumerator WaitForAnimationsAndResetRoutine()
    {
        yield return new WaitForSeconds(cardCloseAnimationDuration);
        ResetCardList();
    }

    public void ResetCardList()
    {
        isOpen = false;
        foreach (GameObject card in instantiatedCards) if (card != null) Destroy(card);
        instantiatedCards.Clear();
    }

    //저장된 이름의 프리팹 가져오기
    public GameObject GetPrefabByName(string cardName)
    {
        // 리스트에서 이름이 일치하는 프리팹 찾기
        GameObject prefab = generalCardPool.Find(p => p.name == cardName);
        if (prefab == null) prefab = legendCardPool.Find(p => p.name == cardName);

        return prefab;
    }
}
