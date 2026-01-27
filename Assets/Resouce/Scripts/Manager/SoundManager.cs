using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("1. 유저 슬라이더 설정값 (0~1)")]
    public float masterVolume;
    public float bgmVolume;
    public float sfxVloume;

    [Header("2. 최종 계산된 결과값 (s 스토리지)")]
    // 이 변수들이 실제 스피커로 나갈 '진짜 볼륨'입니다.
    public float s_master;
    public float s_bgm;
    public float s_sfx;

    #region 싱글톤
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Update()
    {
        // 일시정지 메뉴가 열려있을 때 실시간으로 값을 가져오고 계산함
        if (UIManager.Instance.isOpenPauseMenu)
        {
            // [입력] UI 슬라이더에서 현재 위치값을 가져옴
            masterVolume = UIManager.Instance.masterSlider.value;
            bgmVolume = UIManager.Instance.bgmSlider.value;
            sfxVloume = UIManager.Instance.sfxSlider.value;

            // [계산] 마스터 볼륨을 기준으로 백분율 계산 후 's'에 저장
            CalculateS_Storage();
        }
    }

    private void CalculateS_Storage()
    {
        // 마스터는 그 자체로 천장 역할
        s_master = masterVolume;

        // 최종 BGM = 마스터(전체 높이) * BGM 설정비율
        s_bgm = masterVolume * bgmVolume;

        // 최종 SFX = 마스터(전체 높이) * SFX 설정비율
        s_sfx = masterVolume * sfxVloume;

        // 맛보기용 확인 로그 (너무 자주 찍히면 주석처리하세요)
        // Debug.Log($"최종 계산값 - BGM: {s_bgm}, SFX: {s_sfx}");
    }
}