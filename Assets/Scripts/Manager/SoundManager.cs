using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;
using VInspector.Libs;

public class SoundManager : Singleton<SoundManager>
{
    //TODO: 환경설정에서 조절한 소리를 적용할 수 있는 부분 추가해야함.
    public float bgmVolume = 0.2f;
    public float seVolme = 0.1f;

    public GameObject BGM_SoundPool;        //BGM소리 오브젝트를 풀링할 오브젝트
    public GameObject SFX_SoundPool;        //SFX소리 오브젝트를 풀링할 오브젝트

    Dictionary<AudioClip, List<AudioSource>> audioPoolsSFX = new();
    Dictionary<AudioClip, AudioSource> audioPoolsBGM = new();

    #region 음악 경로 지정 관련 
    public AudioClip menuBGM;               //메인 메뉴 BGM
    public AudioClip battleBGM;             //베틀 메뉴 BGM

    public AudioClip playerOnDamageSFX;     //플레이어 피격 SFX
    public AudioClip playerWalkSFX;         //플레이어 걸음 SFX
    public AudioClip playerJumpSFX;         //플레이어 점프 SFX
    public AudioClip playerRunSFX;          //플레이어 뛰기 SFX
    public AudioClip playerDieSFX;          //플레이어 사망 SFX
    public AudioClip playerLevelUpSFX;      //플레이어 레벨 업 SFX
    public AudioClip enemyOnDamageSFX;      //몬스터 피격 SFX
    public AudioClip itemEquippedSFX;       //아이템 장착 SFX
    public AudioClip itemUpgradeSFX;        //아이템 강화 SFX
    public AudioClip useItemUseSFX;         //아이템 사용 SFX
    public AudioClip dungeonClearSFX;       //던전 클리어 SFX
    #endregion

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        CreatePoolObject();                 //풀링하는 오브젝트를 한꺼번에 저장할 오브젝트 생성
        initAudioClip();                    //Clip들의 Resource경로 설정
    }


    //풀링하는 오브젝트를 한꺼번에 저장할 오브젝트 생성
    private void CreatePoolObject()
    {
        BGM_SoundPool = new GameObject("SoundPoolBGM");
        SFX_SoundPool = new GameObject("SoundPoolSFX");
        BGM_SoundPool.transform.parent = transform;
        SFX_SoundPool.transform.parent = transform;
    }


    //오디오 클립을 초기화
    private void initAudioClip()
    {
        menuBGM = Resources.Load<AudioClip>("SoundData/MenuBGM");
        battleBGM = Resources.Load<AudioClip>("SoundData/BattleBGM");
        playerOnDamageSFX = Resources.Load<AudioClip>("SoundData/PlayerOnDamage");
        playerWalkSFX = Resources.Load<AudioClip>("SoundData/Walk");
        playerJumpSFX = Resources.Load<AudioClip>("SoundData/Jump");
        playerRunSFX = Resources.Load<AudioClip>("SoundData/Run");
        playerDieSFX = Resources.Load<AudioClip>("SoundData/PlayerDie");
        playerLevelUpSFX = Resources.Load<AudioClip>("SoundData/PlayerLevelUp");
        enemyOnDamageSFX = Resources.Load<AudioClip>("SoundData/EnemyOnDamage");
        itemEquippedSFX = Resources.Load<AudioClip>("SoundData/ItemEquipped");
        itemUpgradeSFX = Resources.Load<AudioClip>("SoundData/ItemUpgrade");
        useItemUseSFX = Resources.Load<AudioClip>("SoundData/PlayerUseUseItem");
        dungeonClearSFX = Resources.Load<AudioClip>("SondData/DungeonClear");
    }


    // 새로운 BGM오브젝트를 생성하며 AudioSource를 반환하는 메서드
    private AudioSource AddAudioBGMObject(AudioClip clip)
    {
        GameObject audioObject = new GameObject("AudioBGM");
        audioObject.transform.SetParent(BGM_SoundPool.transform);
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.clip = clip;
        audioSource.Play();
        return audioSource;
    }


    // 새로운 SFX오브젝트를 생성하며 AudioSource를 반환하는 메서드
    private AudioSource AddAudioSFXObject(AudioClip clip)
    {
        GameObject audioObject = new GameObject("AudioSFX");
        audioObject.transform.SetParent(SFX_SoundPool.transform);
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = false;
        audioSource.Play();
        return audioSource;
    }


    /// <summary>
    /// SFX효과음을 재생할 때 실행할 메서드
    /// </summary>
    /// <param name="clip">재생할 클립을 선언</param>
    private void StartAudioSFX(AudioClip clip)
    {
        ///딕셔너리에서 해당 오디오데이터에 대한 정보를 가져옴.
        ///해당 딕셔너리의 List<AudioSource>에 접근해서 해당 AudioSource에 요소의 맨 앞이 isPlay중인지 검사함.
        ///isPlay중이 아니라면 해당 오디오를 다시 재생시키고 리스트의 맨 뒤로 이동시킴.
        ///만일 맨 앞에 있는 AudioSource가 재생중이라면 다른 모든 AudioSource도 재생 중이라는 뜻이니
        ///해당 오디오 소스를 하나 새로 추가해서 재생 시킨 후 맨 뒤에 추가함.
        if (audioPoolsSFX.ContainsKey(clip))
        {
            List<AudioSource> tempList = audioPoolsSFX[clip];
            if (tempList != null &&
              !(tempList[0].isPlaying))
            {
                tempList[0].Play();
                tempList.MoveFirstToLastList();
            }
            else
            {
                tempList.Add(AddAudioSFXObject(clip));
            }
        }
        else
        {
            List<AudioSource> tempList = new List<AudioSource>();
            tempList.Add(AddAudioSFXObject(clip));
            audioPoolsSFX.Add(clip, tempList);
        }
    }


    /// <summary>
    /// BGM을 재생할 때 사용하는 메서드
    /// </summary>
    /// <param name="clip">재생할 클립을 선언</param>
    private void StartAudioBGM(AudioClip clip)
    {
        ///BGM 오브젝트의 풀링은 SFX와 다르게 실행될 필요가 있음.
        ///BGM은 하나를 초과하는 오디오들이 실행될 경우 BGM이 겹치는 문제가 발생함.
        ///그러므로 BGM쪽은 따로 Dictionary<AudioClip, AudioSource>로 관리하는 쪽이 유리함.
        ///왜냐하면 어차피 Loof로 계속 돌게 될탠데 List로 추가할 필요없기 때문.
        ///BGM부분은 해당 Transform을 기준으로 정렬.
        ///BGM을 관리하는 게임오브젝트의 자식 오브젝트가 있는지 확인.
        ///있다면 0번째에 있는 오브젝트의 AudioSource를 가져와서 멈춰줌.
        ///특정 AudioSource를 가지고 있는 GameObject의 위치를 BGM을 관리하는 오브젝트의 0번째로 올림.
        ///이후 해당 AudioSource를 활성화 시켜둠.
        
        if(BGM_SoundPool.transform.childCount > 0)
        {
            AudioSource nowPlayAudio = BGM_SoundPool.transform?.GetChild(0)?.GetComponent<AudioSource>();
            if (nowPlayAudio != null) nowPlayAudio.Stop();
        }

        if (audioPoolsBGM.ContainsKey(clip))
        {
            AudioSource tempAudioSource = audioPoolsBGM[clip];
            //0번째의 요소가 재생이 끝난 상태라면 해당 오디오를 실행시키고 맨 앞으로 옮김.
            tempAudioSource.Play();
            tempAudioSource.transform.SetSiblingIndex(0);
        }
        else
        {
            AudioSource tempAudio = AddAudioBGMObject(clip);
            audioPoolsBGM.Add(clip, tempAudio);
            tempAudio.transform.SetSiblingIndex(0);
        }
    }


    //딜레이로 소리를 출력할 때 실행할 코루틴
    private IEnumerator DelayStartAudio(Action action, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        action?.Invoke();
    }


    #region BGM 출력 관련

    /// <summary>
    /// BGM MainMenu을 실행할 메서드
    /// </summary>
    public void StartAudioBGM_Mainmenu()
    {
        StartAudioBGM(menuBGM);
    }
    /// <param name="delayTime">스타트에 지연을 주고 싶은 시간을 입력</param>
    public void StartAudioBGM_Mainmenu(float delayTime)
    {
        StartCoroutine(DelayStartAudio(StartAudioBGM_Mainmenu, delayTime));
    }


    /// <summary>
    /// BGM battle을 실행할 메서드
    /// </summary>
    public void StartAudioBGM_Battle()
    {
        StartAudioBGM(battleBGM);
    }
    /// <param name="delayTime">스타트에 지연을 주고 싶은 시간을 입력</param>
    public void StartAudioBGM_Battle(float delayTime)
    {
        StartCoroutine(DelayStartAudio(StartAudioBGM_Battle, delayTime));
    }
    #endregion


    //TODO: BGM 출력 멈춤 메서드 필요할지도?


    #region SFX 출력 관련
    /// <summary>
    /// SFX PlayerDie을 실행할 메서드
    /// </summary>
    public void StartAudioSFX_PlayerDie()
    {
        StartAudioSFX(playerDieSFX);
    }
    /// <param name="delayTime">스타트에 지연을 주고 싶은 시간을 입력</param>
    public void StartAudioSFX_PlayerDie(float delayTime)
    {
        StartCoroutine(DelayStartAudio(StartAudioSFX_PlayerDie, delayTime));
    }


    /// <summary>
    /// SFX UseItemUse을 실행할 메서드
    /// </summary>
    public void StartAudioSFX_UseItemUse()
    {
        StartAudioSFX(useItemUseSFX);
    }
    /// <param name="delayTime">스타트에 지연을 주고 싶은 시간을 입력</param>
    public void StartAudioSFX_UseItemUse(float delayTime)
    {
        StartCoroutine(DelayStartAudio(StartAudioSFX_UseItemUse, delayTime));
    }


    /// <summary>
    /// SFX DungeonClear을 실행할 메서드
    /// </summary>
    public void StartAudioSFX_DungeonClear()
    {
        StartAudioSFX(dungeonClearSFX);
    }
    /// <param name="delayTime">스타트에 지연을 주고 싶은 시간을 입력</param>
    public void StartAudioSFX_DungeonClear(float delayTime)
    {
        StartCoroutine(DelayStartAudio(StartAudioSFX_DungeonClear, delayTime));
    }


    /// <summary>
    /// SFX ItemEquipped을 실행할 메서드
    /// </summary>
    public void StartAudioSFX_ItemEquipped()
    {
        StartAudioSFX(itemEquippedSFX);
    }
    /// <param name="delayTime">스타트에 지연을 주고 싶은 시간을 입력</param>
    public void StartAudioSFX_ItemEquipped(float delayTime)
    {
        StartCoroutine(DelayStartAudio(StartAudioSFX_ItemEquipped, delayTime));
    }


    /// <summary>
    /// SFX EnemyOnDamage을 실행할 메서드
    /// </summary>
    public void StartAudioSFX_EnemyOnDamage()
    {
        StartAudioSFX(enemyOnDamageSFX);
    }
    /// <param name="delayTime">스타트에 지연을 주고 싶은 시간을 입력</param>
    public void StartAudioSFX_EnemyOnDamage(float delayTime)
    {
        StartCoroutine(DelayStartAudio(StartAudioSFX_EnemyOnDamage, delayTime));
    }


    /// <summary>
    /// SFX ItemUpgrade을 실행할 메서드
    /// </summary>
    public void StartAudioSFX_ItemUpgrade()
    {
        StartAudioSFX(itemUpgradeSFX);
    }
    /// <param name="delayTime">스타트에 지연을 주고 싶은 시간을 입력</param>
    public void StartAudioSFX_ItemUpgrade(float delayTime)
    {
        StartCoroutine(DelayStartAudio(StartAudioSFX_ItemUpgrade, delayTime));
    }


    /// <summary>
    /// SFX PlayerJump을 실행할 메서드
    /// </summary>
    public void StartAudioSFX_PlayerJump()
    {
        StartAudioSFX(playerJumpSFX);
    }
    /// <param name="delayTime">스타트에 지연을 주고 싶은 시간을 입력</param>
    public void StartAudioSFX_PlayerJump(float delayTime)
    {
        StartCoroutine(DelayStartAudio(StartAudioSFX_PlayerJump, delayTime));
    }


    /// <summary>
    /// SFX PlayerLevelUp을 실행할 메서드
    /// </summary>
    public void StartAudioSFX_PlayerLevelUp()
    {
        StartAudioSFX(playerLevelUpSFX);
    }
    /// <param name="delayTime">스타트에 지연을 주고 싶은 시간을 입력</param>
    public void StartAudioSFX_PlayerLevelUp(float delayTime)
    {
        StartCoroutine(DelayStartAudio(StartAudioSFX_PlayerLevelUp, delayTime));
    }


    /// <summary>
    /// SFX PlayerOnDamage을 실행할 메서드
    /// </summary>
    public void StartAudioSFX_PlayerOnDamage()
    {
        StartAudioSFX(playerOnDamageSFX);
    }
    /// <param name="delayTime">스타트에 지연을 주고 싶은 시간을 입력</param>
    public void StartAudioSFX_PlayerOnDamage(float delayTime)
    {
        StartCoroutine(DelayStartAudio(StartAudioSFX_PlayerOnDamage, delayTime));
    }


    /// <summary>
    /// SFX PlayerWalk을 실행할 메서드
    /// </summary>
    public void StartAudioSFX_PlayerWalk()
    {
        StartAudioSFX(playerWalkSFX);
    }
    /// <param name="delayTime">스타트에 지연을 주고 싶은 시간을 입력</param>
    public void StartAudioSFX_PlayerWalk(float delayTime)
    {
        StartCoroutine(DelayStartAudio(StartAudioSFX_PlayerWalk, delayTime));
    }
    #endregion


    //TODO: 모든 SFX 출력 멈춤. 모든 List<AudioSource>를 0번부터 순회하여 IsPlay가 false인 녀석이 나온다면 빠져나감.
    //TODO: 실행되고 있는 모든 코루틴도 종료시켜야함.


    ///README
    ///사운드를 출력하고 싶을 경우
    ///BGM 출력 관련 및 SFX 출력 관련 메서드를 확인해서 출력하고 싶은 Clip이 들어있는 메서드를 확인.
    ///출력과 관련된 메서드들은 오버로드가 되어있는 부분이 있음.
    ///float delayTime에 매개변수를 넣을 경우 해당 시간 이후에 소리가 출력될 수 있도록 시간 조절 가능.
}
