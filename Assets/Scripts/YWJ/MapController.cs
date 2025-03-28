using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject MapPrefab;
    private List<GameObject> currentMaps = new List<GameObject>();
    private List<Vector3> currentMapPos; 
    public Action ChangeMap;
    public Vector3 playerMapPosition = Vector3.zero;

    private void Init()
    {
        currentMapPos = new List<Vector3>()
        {
            playerMapPosition,
            playerMapPosition + Vector3.forward*16f,
            playerMapPosition + Vector3.back*16f,
            playerMapPosition + Vector3.right*16f,
            playerMapPosition + Vector3.left*16f,
            playerMapPosition + new Vector3(1,0,1)*16f,
            playerMapPosition + new Vector3(-1,0,1)*16f,
            playerMapPosition + new Vector3(1,0,-1)*16f,
            playerMapPosition + new Vector3(-1,0,-1)*16f
        };

        foreach (var map in currentMapPos)
        {
            currentMaps.Add(Instantiate(MapPrefab, map, Quaternion.identity));
        }
        ChangeMap += ChangeMaps;
    }
    private void Start()
    {
        Init();
    }

    private void ChangeMaps()
    {
        currentMapPos = new List<Vector3>()
        {
            playerMapPosition,
            playerMapPosition + Vector3.forward*16f,
            playerMapPosition + Vector3.back*16f,
            playerMapPosition + Vector3.right*16f,
            playerMapPosition + Vector3.left*16f,
            playerMapPosition + new Vector3(1,0,1)*16f,
            playerMapPosition + new Vector3(-1,0,1)*16f,
            playerMapPosition + new Vector3(1,0,-1)*16f,
            playerMapPosition + new Vector3(-1,0,-1)*16f
        };

        List<Vector3> changePos = currentMapPos.Except(currentMaps.Select(x => x.transform.position)).ToList();
        List<GameObject> changeMap = currentMaps.Where(obj => !currentMapPos.Contains(obj.transform.position)).ToList();

        for (int i = 0; i < changeMap.Count; i++)
        {
            if(changeMap.Count > i)
                changeMap[i].transform.position = changePos[i];
        }
    }




}
