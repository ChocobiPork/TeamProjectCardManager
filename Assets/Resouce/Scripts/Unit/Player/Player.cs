using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerLevel;
    public bool isPlayerTurn; //현재 턴의 오너가 플레이어면 True;

    [Header("플레이어 사운드")]
    public AudioClip fireAudioClip;

    public void Update()
    {
        TurnSynchronization();
        LevelSynchronization();
        Testing();
    }


    //아래 함수들은 동기화라기보단 플레이어에게 직접적으로 보여주는 역할을 함

    public void TurnSynchronization() //플레이어의 턴 동기화 -> 동기화라 해야할지 모르겠음
    {
        if (GameManager.Instance.TurnMgr.currentTurnOwner == TurnManager.TurnOwner.Player.ToString())
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
        playerLevel = GameManager.Instance.LevelMgr._currentPlayerLevel;
    }

    //테스트용
    public void Testing()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.SoundMgr.SoundPlay("sfx", "PlayerFireSound", fireAudioClip);
            Debug.Log("플레이어 사운드 재생 테스트");
        }
    }
}
