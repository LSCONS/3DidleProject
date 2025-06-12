using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private Player player;
    public Player Player
    {
        get 
        {
            if(player == null)
            {
                player = FindFirstObjectByType<Player>();
            }
            return player; 
        }
    }

    public Transform PlayerTransform => Player?.transform;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
