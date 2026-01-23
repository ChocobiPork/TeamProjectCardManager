using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerLevel;
    public bool isPlayerTurn; //현재 턴의 오너가 플레이어면 True;

    public void Update()
    {
        TurnSynchronization();
        LevelSynchronization();
    }


    //아래 함수들은 동기화라기보단 플레이어에게 직접적으로 보여주는 역할을 함

    public void TurnSynchronization() //플레이어의 턴 동기화 -> 동기화라 해야할지 모르겠음
    {
        if (TurnManager.instance.currentTurnOwner == TurnManager.TurnOwner.Player.ToString())
        {
            isPlayerTurn = true;
        }
        else
        {
            isPlayerTurn = false;
        }
    }

    //레벨 동기화
    public void LevelSynchronization()
    {
        playerLevel = LevelManager.instance._currentPlayerLevel;
    }
}
