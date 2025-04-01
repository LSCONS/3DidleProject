using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitBladePool : MonoBehaviour
{
    private static OrbitBladePool instance;
    public static OrbitBladePool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("OrbitBladePool").AddComponent<OrbitBladePool>();
            }
            return instance;
        }
    }


    public GameObject bladePrefab;
    public int initalPoolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < initalPoolSize; i++)
        {
            GameObject blade = Instantiate(bladePrefab, this.transform);
            blade.SetActive(false);
            pool.Enqueue(blade);
        }
    }

    public GameObject GetBlade()
    {
        if (pool.Count > 0)
        {
            GameObject blade = pool.Dequeue();
            blade.SetActive(true);
            return blade;
        }
        else
        {
            GameObject newBlade = Instantiate(bladePrefab);
            return newBlade;
        }
    }

    public void ReturnBlade(GameObject blade)
    {
        blade.SetActive(false);
        pool.Enqueue(blade);
    }

}
