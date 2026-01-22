using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableCard : MonoBehaviour
{
    void Update()
    {
        // 마우스 왼쪽 버튼이 눌렸을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치에서 화면을 통과하는 Ray를 만듦.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Ray가 3D 오브젝트에 충돌 했는지 확인
            if (Physics.Raycast(ray, out hit))
            {
                // 충돌한 오브젝트의 이름을 콘솔에 출력.
                Debug.Log("Raycast로 오브젝트 클릭 감지됨! 충돌 오브젝트: " + hit.collider.gameObject.name);

                // 특정 태그를 가진 오브젝트만 감지(예 : Card 태그)
                if (hit.collider.CompareTag("Card"))
                {
                    Debug.Log("클릭 가능한 오브젝트입니다.");
                }
            }
        }
    }
}
