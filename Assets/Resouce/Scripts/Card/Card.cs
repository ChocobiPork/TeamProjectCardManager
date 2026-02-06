using UnityEngine;

public class Card : MonoBehaviour
{
    public enum CardRarity
    {
        General,
        Legend
    }

    // 카드가 실제로 가질 등급을 저장하는 변수
    public CardRarity rarity;
}