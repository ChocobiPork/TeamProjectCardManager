using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("설정")]
    public GameObject boomerangPrefab;
    [Range(1, 5)] public int boomerangLevel = 1; // 테스트를 위해 인스펙터에서 조절 가능

    // 레벨에 따른 방향 정의 (상, 하, 좌, 우, 좌상, 좌하, 우상, 우하)
    private Vector2[] shotDirections = new Vector2[]
    {
        Vector2.up,          // 1레벨: 상
        Vector2.left,        // 2레벨: 하
        Vector2.right,        // 3레벨: 좌
        new Vector2(-1,-1),     // 4레벨: 우
        new Vector2(1,-1),  // 5레벨: 좌상
    };

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.CardMgr.isOpen)
        {
            ShootBoomerangs();
        }
    }

    void ShootBoomerangs()
    {
        if (boomerangPrefab == null) return;

        // 현재 레벨만큼 반복문 실행
        int count = Mathf.Clamp(boomerangLevel, 1, shotDirections.Length);

        for (int i = 0; i < count; i++)
        {
            // 1. 부메랑 생성
            GameObject go = Instantiate(boomerangPrefab, transform.position, Quaternion.identity);

            // 2. 해당 순서의 방향 가져오기
            Vector2 dir = shotDirections[i];

            // 3. 발사
            Boomerang boomerang = go.GetComponent<Boomerang>();
            if (boomerang != null)
            {
                boomerang.Shot(dir,this.transform);
            }
        }
    }
}