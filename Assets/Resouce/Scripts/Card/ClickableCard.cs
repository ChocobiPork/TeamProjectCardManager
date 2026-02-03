using UnityEngine;

public class ClickableCard : MonoBehaviour
{
    void Update()
    {
        CardClickable();
    }

    private void CardClickable()
    {
        // 마우스 왼쪽 버튼이 눌렸고 카드가 보여지고 있을때
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.UIMgr.isOpenPauseMenu != true)
        {
            // 마우스 위치에서 화면을 통과하는 Ray를 만듦.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Ray가 3D 오브젝트에 충돌 했는지 확인
            if (Physics.Raycast(ray, out hit))
            {
                // 특정 태그를 가진 오브젝트만 감지 -> 예 : Card 태그
                if (hit.collider.CompareTag("Card") && GameManager.Instance.CardMgr.isOpen)
                {
                    Debug.Log($"현재 클릭한 오브젝트 : {hit.collider.gameObject.name}");

                    // ResetCardList() 대신, 클릭된 오브젝트를 넘겨주면서 닫기 함수 호출
                    GameManager.Instance.CardMgr.CloseOtherCards(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Card"))
                {
                    Debug.Log("현재 선택된 항목은 카드지만 카드가 열려있지 않습니다.");
                }
                else
                {
                    Debug.Log("뭘 누르고 있는거임");
                }
            }
        }
    }
}