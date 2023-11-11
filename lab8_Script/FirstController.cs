using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class FirstController : MonoBehaviour, ISceneController
{

    public GameruleAttribute rule;
    public ObjectPoolManager pool { get; set; }
    private IReferee referee{get;set;}
    //需要在unity中将主摄像机拖入
    public GameObject cam;
    //MVC的模型部分
    int round;
    int curDishNum;
    public int activeDish;
    public int curtrail;
    public bool gameover;
    public void Awake()
    {
        SSDirector d = SSDirector.getInstance();
        d.currentSceneController = this;
        pool=ObjectPoolManager.getInstance();
        referee = Referee.getInstance();
        referee.SetController(this);
        LoadResources();
        Reset();
    }
    public void Reset()
    {
        curDishNum = rule.initNum;
        activeDish = 0;
        curtrail = 0;
        gameover = false;
    }
    public void LoadResources()
    {
        GameObject newObj = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/bound"));
    }

    public void GameOver()
    {
        gameover = true;
    }

    public void GameRestart()
    {
        Reset();
        referee.Restart();
    }
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 20;
        string text = "分数 " + referee.getScore().ToString();
        Rect rect = new Rect(10, 35, 200, 200);

        // 绘制文本
        GUI.Label(rect, text,style);
        if (gameover)
        {
            string str="Restart";
            if (GUI.Button(new Rect(10, 10, 100, 30), str))
            {
                GameRestart();
            }
        }
        else
        {
            text = "回合 " + curtrail.ToString();
            // 设置文本的位置和大小
            rect = new Rect(10, 10, 200, 200);
            // 绘制文本
            GUI.Label(rect, text, style);
        }
    }

    //MVC的视图部分
    void Update()
    {

        if (gameover)
        {
            return;
        }
        referee.check();
        if (Input.GetButtonDown("Fire1"))
        {
            // Debug.Log("Fired Pressed");
            // Debug.Log(Input.mousePosition);

            Vector3 mp = Input.mousePosition; //get Screen Position

            //create ray, origin is camera, and direction to mousepoint
            Camera ca;
            if (cam != null) ca = cam.GetComponent<Camera>();
            else ca = Camera.main;

            Ray ray = ca.ScreenPointToRay(Input.mousePosition);

            //Return the ray's hit
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                referee.ShootDish(hit.transform.gameObject);
            }
        }

    }

    //MVC的控制部分
    public void startNextTurn()
    {
        if (curtrail % rule.increaseRound == 0)
        {
            curDishNum += rule.increaseNum;
        }
        activeDish = curDishNum;
        for (int i = 0; i < curDishNum; i++)
        {
            throwDish();
        }
    }

    public void throwDish()
    {
        int r = UnityEngine.Random.Range(1, 101);
        GameObject dish;
        int speed;
        if (r >= rule.Rate3)
        {
            dish = pool.GetObjectFromPool3();
            speed = rule.Speed3;
        }else if(r>= rule.Rate2)
        {
            dish = pool.GetObjectFromPool2();
            speed = rule.Speed2;
        }
        else
        {
            dish = pool.GetObjectFromPool1();
            speed = rule.Speed1;
        }
        r = UnityEngine.Random.Range(1,4);
        float weight= UnityEngine.Random.Range(0f, 1f);
        Vector3 p1 = new Vector3(0, 0, -680), p2 = new Vector3(0, -300, -680), p3 = new Vector3(0, -300, 680), p4 = new Vector3(0, 0, 680);
        switch (r)
        {
            case 1:
                dish.GetComponent<Rigidbody>().position = weight * p1 + (1 - weight) * p2;
                break;
            case 2:
                dish.GetComponent<Rigidbody>().position = weight * p2 + (1 - weight) * p3;
                break;
            case 3:
                dish.GetComponent<Rigidbody>().position = weight * p3 + (1 - weight) * p4;
                break;
        }
        float targetz= UnityEngine.Random.Range(-150,150);
        float targety = UnityEngine.Random.Range(100, 300);
        Vector3 target = new Vector3(0,targety,targetz);
        Vector3 direction = (target - dish.GetComponent<Rigidbody>().position).normalized;
        dish.GetComponent<Rigidbody>().velocity = direction * speed;
    }

    public void RemoveCollision(GameObject dish)
    {
        pool.ReturnObjectToPool(dish);
        activeDish--;
    }
}