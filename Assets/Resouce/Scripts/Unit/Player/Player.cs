using UnityEngine;
using System.Linq; // Count를 쓰기 위해 필수!

public class Player : MonoBehaviour
{
    [Header("설정")]
    public GameObject boomerangPrefab;

    // 이제 이 변수가 '진짜' 레벨 저장소입니다.
    // 카드를 먹으면 코드가 알아서 이 숫자를 바꿔줄 겁니다.
    public int boomerangLevel = 0;

    private Vector2[] shotDirections = new Vector2[]
    {
        Vector2.up,                          // 1: 상
        Vector2.left,                        // 2: 좌
        Vector2.right,                       // 3: 우
        new Vector2(-1, -1).normalized,      // 4: 좌하
        new Vector2(1, -1).normalized        // 5: 우하
    };

    void Update()
    {
        // 1. 발사 전에 현재 카드 인벤토리 상황을 내 변수에 동기화
        UpdateBoomerangLevel();

        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.CardMgr.isOpen)
        {
            ShootBoomerangs();
        }
    }

    void UpdateBoomerangLevel()
    {
        if (GameManager.Instance.CardMgr == null) return;

        // CardManager의 리스트에서 내 부메랑 카드 개수를 세서 변수에 저장
        // 이렇게 하면 인스펙터에서도 숫자가 올라가는 게 보입니다.
        boomerangLevel = GameManager.Instance.CardMgr.selectCardNames.Count(name => name == "BoomerangCard");
    }

    void ShootBoomerangs()
    {
        if (boomerangPrefab == null) return;

        // 2. 이제 헷갈릴 것 없이 무조건 내 변수(boomerangLevel)만 봅니다.
        if (boomerangLevel <= 0) return;

        int count = Mathf.Min(boomerangLevel, shotDirections.Length);

        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(boomerangPrefab, transform.position, Quaternion.identity);
            Boomerang bScript = go.GetComponent<Boomerang>();

            if (bScript != null)
            {
                bScript.Shot(shotDirections[i], this.transform);
            }
        }
    }
}