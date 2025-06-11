using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.StartAudioBGM_Battle();
    }
}
