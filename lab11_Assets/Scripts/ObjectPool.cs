using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Pool;

public class ObjectPoolManager
{
    public static ObjectPoolManager Instance { get; private set; }
    public int initialPoolSize1 = 10; // 初始对象池大小
    public int initialPoolSize2 = 10; // 初始对象池大小
    public int initialPoolSize3 = 10; // 初始对象池大小

    private List<GameObject> objectPool1; // 对象池
    private List<GameObject> objectPool2; // 对象池
    private List<GameObject> objectPool3; // 对象池

    public static ObjectPoolManager getInstance()//提供单例的访问
    {
        if (Instance == null)
        {
            Instance = new ObjectPoolManager();
        }
        return Instance;
    }

    public ObjectPoolManager()
    {
        objectPool1 = new List<GameObject>();
        objectPool2 = new List<GameObject>();
        objectPool3 = new List<GameObject>();
        for (int i = 0; i < initialPoolSize1; i++)
        {
            GameObject obj = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/dish1"));
            obj.SetActive(false);
            obj.name = "score1";
            obj.GetComponent<Rigidbody>().position =new Vector3(-10000, 0, 0);
            objectPool1.Add(obj);
        }
        for (int i = 0; i < initialPoolSize2; i++)
        {
            GameObject obj = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/dish2"));
            obj.SetActive(false);
            obj.name = "score2";
            obj.GetComponent<Rigidbody>().position = new Vector3(-10000, 0, 0);
            objectPool2.Add(obj);
        }
        for (int i = 0; i < initialPoolSize3; i++)
        {
            GameObject obj = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/dish3"));
            obj.SetActive(false);
            obj.name = "score3";
            obj.GetComponent<Rigidbody>().position = new Vector3(-10000, 0, 0);
            objectPool3.Add(obj);
        }
    }

    public GameObject GetObjectFromPool1()
    {
        foreach (GameObject obj in objectPool1)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        // 如果没有可用对象，则动态创建一个新对象
        GameObject newObj = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/dish1"));
        newObj.name = "score1";
        newObj.GetComponent<Rigidbody>().position = new Vector3(-10000, 0, 0);
        objectPool1.Add(newObj);
        return newObj;
    }

    public GameObject GetObjectFromPool2()
    {
        foreach (GameObject obj in objectPool2)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        // 如果没有可用对象，则动态创建一个新对象
        GameObject newObj = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/dish2"));
        newObj.name = "score2";
        newObj.GetComponent<Rigidbody>().position = new Vector3(-10000, 0, 0);
        objectPool2.Add(newObj);
        return newObj;
    }

    public GameObject GetObjectFromPool3()
    {
        foreach (GameObject obj in objectPool3)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        // 如果没有可用对象，则动态创建一个新对象
        GameObject newObj = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/dish3"));
        newObj.name = "score3";
        newObj.GetComponent<Rigidbody>().position = new Vector3(-10000, 0, 0);
        objectPool3.Add(newObj);
        return newObj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}