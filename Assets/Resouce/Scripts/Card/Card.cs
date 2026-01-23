using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int CardNumber; //카드 번호 -> 나중에 카드 넘버로 속성 받게끔
    public string CardName; //카드 이름

    //기능
    // 카드는 여러개니까 Sprite,Info,Name 등을 csv or json으로 받아오기 <- csv 파일은 내가 가지고 있지 않으니 해당사항 x
    // 카드 선택시 CardManager에서 Info를 받음. (현재는 카드 번호와 카드 이름만.)
}
