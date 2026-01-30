using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableCard : MonoBehaviour
{
    void Update()
    {
        CardClickable();
    }

    private void CardClickable()
    {
        // 마우스 왼쪽 버튼이 눌렸고 카드가 보여지고 있을때 -> 플레이어가 카드 선택이 아닐땐 작동하지 않도록 트리거 설정
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.CardMgr.showCard && GameManager.Instance.UIMgr.isOpenPauseMenu != true)
        {
            // 마우스 위치에서 화면을 통과하는 Ray를 만듦.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //카메라(스크린 -> 보여지는 화면)기준으로 마우스 입력을 받음
            RaycastHit hit;

            // Ray가 3D 오브젝트에 충돌 했는지 확인
            if (Physics.Raycast(ray, out hit)) //물리적 Ray
            {
                // 충돌한 오브젝트의 이름을 콘솔에 출력.
                //Debug.Log("Raycast로 오브젝트 클릭 감지됨 충돌 오브젝트: " + hit.collider.gameObject.name);

                // 특정 태그를 가진 오브젝트만 감지 -> 예 : Card 태그
                if (hit.collider.CompareTag("Card") && GameManager.Instance.CardMgr.isOpen) //카드가 열려있고 클릭한 Object가 카드라면.
                {
                    Debug.Log($"현재 클릭한 오브젝트 : {hit.collider.gameObject.name}");
                    GameManager.Instance.CardMgr.HideCard(); //카드를 선택한것이니 카드 숨김
                }
                else if (hit.collider.CompareTag("Card")) //만약 카드태그 값만 받았다면
                {
                    Debug.Log("현재 선택된 항목은 카드지만 카드가 열려있지 않습니다.");
                }
                else //카드 태그도 아니고 오픈도 아니라면
                {
                    Debug.Log("뭘 누르고 있는거임");
                }
            }
        }
    }
}
