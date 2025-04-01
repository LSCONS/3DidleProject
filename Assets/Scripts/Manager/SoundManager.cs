using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;
using VInspector.Libs;

public class SoundManager : Singleton<SoundManager>
{
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
        initAudioClip();        //Clip들의 Resource경로 설정
    }


    private void initAudioClip()
    {
        menuBGM = Resources.Load<AudioClip>("SoundData/MenuBGM");
        battleBGM = Resources.Load<AudioClip>("SoundData/BattleBGM");
        playerOnDamageSFX = Resources.Load<AudioClip>("SoundData/PlayerOnDamage");
        playerWalkSFX = Resources.Load<AudioClip>("SoundData/Walk");
        playerJumpSFX = Resources.Load<AudioClip>("SoundData/Jump");
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


    public void StartAudioSFX(AudioClip clip)
    {
        ///딕셔너리에서 해당 오디오데이터에 대한 정보를 가져옴.
        ///해당 딕셔너리의 List<AudioSource>에 접근해서 해당 AudioSource에 요소의 맨 앞이 isPlay중인지 검사함.
        ///isPlay중이 아니라면 해당 오디오를 다시 재생시키고 리스트의 맨 뒤로 이동시킴.
        ///만일 맨 앞에 있는 AudioSource가 재생중이라면 다른 모든 AudioSource도 재생 중이라는 뜻이니
        ///해당 오디오 소스를 하나 새로 추가해서 재생 시킨 후 맨 뒤에 추가함.
        if (audioPoolsSFX.ContainsKey(clip))
        {
            List<AudioSource> tempList = audioPoolsSFX[clip];
            Debug.Log("111" + (tempList != null));
            Debug.Log("222" + !tempList[0].isPlaying);
            if (tempList != null &&
              !(tempList[0].isPlaying))   //0번째의 요소가 재생이 끝난 상태라면 플레이시키고 맨 뒤로 옮김
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


    public void StartAudioBGM(AudioClip clip)
    {
        ///BGM 오브젝트의 풀링은 SFX와 다르게 실행될 필요가 있음.
        ///BGM은 하나를 초과하는 오디오들이 실행될 경우 BGM이 겹치는 문제가 발생함.
        ///그러므로 BGM쪽은 따로 Dictionary<AudioClip, AudioSource>로 관리하는 쪽이 유리함.
        ///왜냐하면 어차피 Loof로 계속 돌게 될탠데 List로 추가할 필요없기 때문.
        ///BGM부분은 해당 Transform을 기준으로 정렬해야할듯?
        ///그렇다면 어떤 유틸이 필요할까
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


#if UNITY_EDITOR
    [Button]
    private void StartMenuAudio()
    {
        StartAudioBGM(menuBGM);
    }
    [Button]
    private void StartBattleAudio()
    {
        StartAudioBGM(battleBGM);
    }

    [Button]
    private void StartOnHitAudio()
    {
        StartAudioSFX(playerOnDamageSFX);
    }
    [Button]
    private void StartLevelUpAudio()
    {
        StartAudioSFX(playerLevelUpSFX);
    }
#endif
}
