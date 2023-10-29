using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FirstController : MonoBehaviour, ISceneController
{
    public CCActionManager actionManager { get; set; }
    private IReferee referee{get;set;}
    //��Ҫ��unity�н������������
    public GameObject cam;
    //MVC��ģ�Ͳ���
    public GameObject boatObject, leftShore, rightShore, river;//����Ԫ�غʹ�
    public float boatPosition;//����z����
    public int boatArea;//����������1Ϊ��2Ϊ�ң�
    public GameObject[] left;//�󰶵Ľ�ɫ
    public GameObject[] right;//�Ұ��Ľ�ɫ
    public GameObject[] boat;//���ϵĽ�ɫ
    int leftCount;
    int rightCount;
    int boatCount;
    public int leftDevil, rightDevil, leftPriest, rightPriest;//���ҵ���ʦ�Ͷ�ħ����
    bool gameover;//��Ϸ����
    bool win;//trueΪӮ��falseΪ��

    public float getBoatPosition()//�ṩ����z�����ѯ
    {
        return boatPosition;
    }

    public void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
        actionManager = GetComponent<CCActionManager>();
    }
    public void Start()
    {
        referee = new Referee();
        referee.SetController(this);
    }
    public void LoadResources()
    {
        left = new GameObject[6];
        right = new GameObject[6];
        boat = new GameObject[2];
        rightCount = 0;
        boatCount = 0;
        leftDevil = 3; rightDevil = 0; leftPriest = 3; rightPriest = 0;
        //������Դ�ļ���������Ϸ����
        Debug.Log("Loadding...");
        GameObject devil = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/devil"));
        devil.GetComponent<GameobjectInfo>().UpdatePosition(1, 0,true);
        left[0] = devil;
        devil.name = "DEVIL";
        GameObject devil2 = Instantiate(devil);
        devil2.GetComponent<GameobjectInfo>().UpdatePosition(1, 1, true);
        left[1] = devil2;
        devil2.name = "DEVIL";
        GameObject devil3 = Instantiate(devil);
        devil3.GetComponent<GameobjectInfo>().UpdatePosition(1, 2, true);
        left[2] = devil3;
        devil3.name = "DEVIL";
        GameObject priest = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/priest"));
        priest.GetComponent<GameobjectInfo>().UpdatePosition(1, 3, true);
        left[3] = priest;
        priest.name = "PRIEST";
        GameObject priest2 = Instantiate(priest);
        priest2.GetComponent<GameobjectInfo>().UpdatePosition(1, 4, true);
        left[4] = priest2;
        priest2.name = "PRIEST";
        GameObject priest3 = Instantiate(priest);
        priest3.GetComponent<GameobjectInfo>().UpdatePosition(1, 5, true);
        left[5] = priest3;
        priest3.name = "PRIEST";
        boatObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/boat"), new Vector3(0, 0, -3), Quaternion.identity);
        boatObject.name = "BOAT";
        boatPosition = -3;
        boatArea = 1;
        leftShore = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/shore"), new Vector3(0, 0, -13), Quaternion.identity);
        rightShore = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/shore"), new Vector3(0, 0, 13), Quaternion.identity);
        river = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/water"), new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void GameOver(bool win)
    {
        Debug.Log("WIN");
        Debug.Log("LOST");
        gameover = true;
        this.win = win;
    }

    public void GameRestart()
    {
        for (int i = 0; i < 6; i++)
        {
            if (left[i])
                Destroy(left[i]);
            if (right[i])
                Destroy(right[i]);
        }
        if (boat[0])
            Destroy(boat[0]);
        if (boat[1])
            Destroy(boat[1]);
        Destroy(leftShore);
        Destroy(rightShore);
        Destroy(river);
        Destroy(boatObject);
        LoadResources();
        gameover = false;
    }
    private void OnGUI()
    {
        if (gameover && boatObject.GetComponent<GameobjectInfo>().clickable)
        {
            string str;
            if (win)
                str = "YOU WIN";
            else str = "YOU LOST";
            if (GUI.Button(new Rect(10, 10, 100, 30), str))
            {
                GameRestart();
            }
        }
    }

    //MVC����ͼ����
    void Update()
    {
        if (gameover)
        {
            return;
        }
        else if (Input.GetButtonDown("Fire1"))
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
                print(hit.transform.gameObject.name);
                ClickOn(hit.transform.gameObject);
            }
        }
    }

    //MVC�Ŀ��Ʋ���
    void ClickOn(GameObject a)
    {   
        if (a.name == "DEVIL" || a.name == "PRIEST")
        {
            if (!a.GetComponent<GameobjectInfo>().clickable)//���е����������ƶ�
                return;
            if (!boatObject.GetComponent<GameobjectInfo>().clickable)//�������˶�
                return;
            GameobjectInfo p = a.GetComponent<GameobjectInfo>();
            if (p.Area == 3)//Area:1-�󰶣�2-�Ұ���3-����
            {
                if (boatArea == 1)
                {
                    int i = 0;
                    while (left[i] != null) i++;
                    GetDown(a, 1, i);
                    leftCount++;
                    boatCount--;
                }
                else if (boatArea == 2)
                {
                    int i = 0;
                    while (right[i] != null) i++;
                    GetDown(a, 2, i);
                    rightCount++;
                    boatCount--;
                }
            }
            else if (p.Area == 1&&boatArea==1)
            {
                int i;
                if (boat[0] == null)
                    i = 0;
                else if (boat[1] == null)
                    i = 1;
                else
                    return;
                leftCount--;
                boatCount++;
                GetOn(a, i);
            }
            else if (p.Area == 2&&boatArea==2)
            {
                int i;
                if (boat[0] == null)
                    i = 0;
                else if (boat[1] == null)
                    i = 1;
                else
                    return;
                rightCount--;
                boatCount++;
                GetOn(a, i);
            }
        }
        else if (a.name == "BOAT")
        {
            if (!a.GetComponent<GameobjectInfo>().clickable)
                return;
            if (boat[0] != null && !boat[0].GetComponent<GameobjectInfo>().clickable)
                return;
            if (boat[1] != null && !boat[1].GetComponent<GameobjectInfo>().clickable)
                return;
            if (boatCount == 0)//����û�˲��ܹ���
                return;

            if (boatArea == 1)
            {
                a.GetComponent<GameobjectInfo>().clickable = false;
                actionManager.Move_to(a, new Vector3(0, 0, 3),5);
                Debug.Log(">>>>");
                boatPosition = 3;
                boatArea = 2;
                if (boat[0] != null)
                {
                    if (boat[0].name == "DEVIL")
                    {
                        leftDevil--;
                        rightDevil++;
                    }
                    else
                    {
                        leftPriest--;
                        rightPriest++;
                    }
                }
                if (boat[1] != null)
                {
                    if (boat[1].name == "DEVIL")
                    {
                        leftDevil--;
                        rightDevil++;
                    }
                    else
                    {
                        leftPriest--;
                        rightPriest++;
                    }
                }
            }
            else if (boatArea == 2)
            {
                a.GetComponent<GameobjectInfo>().clickable = false;
                actionManager.Move_to(a, new Vector3(0, 0, -3),5);
                Debug.Log("<<<<");
                boatPosition = -3;
                boatArea = 1;
                if (boat[0] != null)
                {
                    if (boat[0].name == "DEVIL")
                    {
                        leftDevil++;
                        rightDevil--;
                    }
                    else
                    {
                        leftPriest++;
                        rightPriest--;
                    }
                }
                if (boat[1] != null)
                {
                    if (boat[1].name == "DEVIL")
                    {
                        leftDevil++;
                        rightDevil--;
                    }
                    else
                    {
                        leftPriest++;
                        rightPriest--;
                    }
                }
            }
            referee.CheckGameOver();
        }
    }

    void GetOn(GameObject a, int boatSeat)//�ϴ�
    {
        a.GetComponent<GameobjectInfo>().clickable = false;
        a.transform.SetParent(boatObject.transform);
        GameobjectInfo p = a.GetComponent<GameobjectInfo>();
        actionManager.Move_3step(a, 3, boatSeat, 3, 5);
        switch (p.Area)
        {
            case 1:
                left[p.Index] = null;
                boat[boatSeat] = a;
                p.UpdatePosition(3, boatSeat,false);
                break;
            case 2:
                right[p.Index] = null;
                boat[boatSeat] = a;
                p.UpdatePosition(3, boatSeat, false);
                break;
        }
    }
    void GetDown(GameObject a, int Area, int shoreSeat)//�´�
    {
        a.GetComponent<GameobjectInfo>().clickable = false;
        a.transform.SetParent(null);
        GameobjectInfo p = a.GetComponent<GameobjectInfo>();
        switch (Area)
        {
            case 1:
                boat[p.Index] = null;
                left[shoreSeat] = a;
                p.UpdatePosition(1, shoreSeat, false);
                actionManager.Move_3step(a, 1, shoreSeat, 3, 5);
                break;
            case 2:
                boat[p.Index] = null;
                right[shoreSeat] = a;
                p.UpdatePosition(2, shoreSeat, false);
                actionManager.Move_3step(a, 2, shoreSeat, 3, 5);
                break;
        }
    }

}