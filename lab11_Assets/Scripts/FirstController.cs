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
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class FirstController : MonoBehaviour, ISceneController
{
    public int Score { get; set; }//��ǰ�÷�
    DateTime lastHitTime;//���һ�����е�ʱ���
    int lastHitTargetValue;//������еİ��ӷ�ֵ
    public int areavalue { get; set; }//��ǰ�������ķ�ֵ
    public static int arrow;//ʣ���������
    public static bool shootArea;//��ǰ�Ƿ����������
    //��Ҫ��unity�н������������
    public GameObject cam;//��Ϸ�����
    private float fieldOfView;//��ǰ��Ұ���
    //MVC��ģ�Ͳ���
    public bool gameover;//��Ϸ�Ƿ����

    public GameObject crossbox;//��Ϸ�е�ʮ�������
    public GameObject Arrowprefab;//����Ԥ����
    private Animator ani;//ʮ����Ķ���������

    public int crosshairSize = 20;//׼����������

    public float zoomSpeed = 5f;//�����л���Ұ��С���ٶ�
    public float minFOV = 20f;//��С��Ұ��
    public float maxFOV = 60f;//�����Ұ��

    private void Start()
    {
    }
    public void Awake()
    {
        SSDirector d = SSDirector.getInstance();
        d.currentSceneController = this;
        ani=crossbox.GetComponent<Animator>();
        ani.SetBool("HasArrow", true);
        arrow = 5000;
        fieldOfView = 60;
    }
    public void Reset()
    {
    }
    public void LoadResources()
    {
    }

    public void GameOver()
    {
        gameover = true;
    }

    public void GameRestart()
    {
        Reset();
    }
    private void OnGUI()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float centerX = screenWidth / 2f;
        float centerY = screenHeight / 2f;
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.normal.textColor = Color.black;
        style.fontSize = 25;
        Rect labelRect = new Rect(screenWidth-100, screenHeight-30, screenWidth, screenHeight);
        GUI.Label(labelRect, "��:"+arrow.ToString(),style);
        labelRect = new Rect(10, 10, 200, 40);
        GUI.Label(labelRect, " ��  �� :"+Score.ToString(),style);
        if (areavalue > 1)
        {
            labelRect = new Rect(10, 40, 200, 80);
            GUI.Label(labelRect, "�÷ֱ���:" + areavalue.ToString(), style);
        }

        if (shootArea) {
            // ����׼����ɫ
            GUI.color = Color.black;

            // ����׼��
            GUI.DrawTexture(new Rect(centerX - crosshairSize / 2f, centerY - 1f, crosshairSize, 2f), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(centerX - 1f, centerY - crosshairSize / 2f, 2f, crosshairSize), Texture2D.whiteTexture);

            // �ָ�GUI��ɫ����
            GUI.color = Color.white;
        }
        if (DateTime.Now < lastHitTime.AddMilliseconds(500))
        {
            string Text = lastHitTargetValue.ToString();
            if(areavalue>1)
                 Text+=" *"+areavalue.ToString();
            labelRect = new Rect(centerX+20, centerY+20, centerX+200, centerY+60);
            GUI.Label(labelRect, Text, style);
            GL.PushMatrix();
            GL.LoadOrtho();
            GL.Begin(GL.LINES);
            GL.Color(new Color(255F / 255F, 0F / 255F, 0F / 255F));
            GL.Vertex3((centerX-crosshairSize) / Screen.width, (centerY-crosshairSize) / Screen.height, 0);
            GL.Vertex3((centerX +crosshairSize) / Screen.width, (centerY + crosshairSize) / Screen.height, 0);
            GL.End();
            GL.Begin(GL.LINES);
            GL.Color(new Color(255F / 255F, 0F / 255F, 0F / 255F));
            GL.Vertex3((centerX - crosshairSize) / Screen.width, (centerY + crosshairSize) / Screen.height, 0);
            GL.Vertex3((centerX + crosshairSize) / Screen.width, (centerY - crosshairSize) / Screen.height, 0);
            GL.End();
            GL.PopMatrix();
        }
    }

    //MVC����ͼ����
    void Update()
    {
        Camera cameraComponent = cam.GetComponent<Camera>();
        // ��ȡ��������
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // ���ݹ������������Ұ��С

        // ������Ұ��Χ����Сֵ�����ֵ֮��
        fieldOfView = Mathf.Clamp(fieldOfView - scrollInput * zoomSpeed, minFOV, maxFOV);
        // Ӧ���µ���Ұ��С

        cameraComponent.fieldOfView = fieldOfView;


    }

    //MVC�Ŀ��Ʋ���
    public void startNextTurn()
    {
    }

    public void Shoot(float speed)
    {
        if (arrow <= 0 || shootArea == false)
            return;
        arrow--;
        if (arrow == 0)
        {
            ani.SetBool("HasArrow", false);
        }
        // ��Ԥ����ʵ������Ϸ����
        GameObject Arrow = Instantiate(Arrowprefab);
        Arrow.name = "Arrow";
        // ����λ�úͳ���
        Arrow.transform.position = cam.transform.position;
        Arrow.transform.rotation = cam.transform.rotation;
        // ��ȡ�������
        Rigidbody rigidbody = Arrow.GetComponent<Rigidbody>();

        // ˲�Ƹ��嵽��λ��
        Vector3 teleportPosition = cam.transform.position + cam.transform.rotation * Vector3.forward * 2;
        rigidbody.MovePosition(teleportPosition);

        // �����ʼ�ٶ�����
        Vector3 initialVelocity = cam.transform.forward * speed;

        // ���ó�ʼ�ٶ�
        rigidbody.velocity = initialVelocity;
        
    }
    public void PlayerCollisionEnter(Collider collision, GameObject msgSource, int msgtype,int value=0)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "FPSController" && msgSource.name == "supply")
        {
            arrow = 10;
            ani.SetBool("HasArrow", true);
        } else if (collision.gameObject.name == "FPSController" && msgSource.name == "shootarea" && msgtype == 1)
        {
            shootArea = true;
            ani.SetBool("ShootArea", true);
            areavalue = value;
        } else if (collision.gameObject.name == "FPSController" && msgSource.name == "shootarea" && msgtype == 2)
        {
            shootArea = false;
            ani.SetBool("ShootArea", false);
            areavalue = 0;
        } else if (collision.gameObject.name == "FPSController" && msgSource.name == "DuskScene" && (msgtype == 3 || msgtype == 4))
        {
            cam.GetComponent<SkyControl>().ChangeBox();
            DynamicGI.UpdateEnvironment();
        }
    }
    public void ArrowCollisionEnter(Collision collision, GameObject msgsource, int targetvalue)
    {
        if (collision.gameObject.name == "Arrow"&&msgsource.name=="target")
        {
            lastHitTargetValue = targetvalue;
            Score += targetvalue*areavalue;
            lastHitTime = DateTime.Now;
        }
    }

}