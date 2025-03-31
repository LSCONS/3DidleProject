using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("PlayerManager").AddComponent<PlayerManager>();
            }
            return instance;
        }
    }

    public Character player;
    public Character Player
    {
        get { return player; }
        set { player = value; }
    }

    public Transform PlayerTransform { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance = this)
            {
                Destroy(gameObject);
            }
        }

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            PlayerTransform = playerObj.transform;
            Player = playerObj.GetComponent<Character>();
        }

    }

}
