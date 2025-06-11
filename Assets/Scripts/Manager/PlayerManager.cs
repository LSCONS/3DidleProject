using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public Player player;
    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    public Transform PlayerTransform { get;  set; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
