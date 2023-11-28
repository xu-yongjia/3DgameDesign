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
    public int Score { get; set; }//当前得分
    DateTime lastHitTime;//最后一次射中的时间戳
    int lastHitTargetValue;//最后射中的靶子分值
    public int areavalue { get; set; }//当前射击区域的分值
    public static int arrow;//剩余箭的数量
    public static bool shootArea;//当前是否处于射击区域
    //需要在unity中将主摄像机拖入
    public GameObject cam;//游戏主相机
    private float fieldOfView;//当前视野宽度
    //MVC的模型部分
    public bool gameover;//游戏是否结束

    public GameObject crossbox;//游戏中的十字弩对象
    public GameObject Arrowprefab;//箭的预制体
    private Animator ani;//十字弩的动作管理器

    public int crosshairSize = 20;//准星线条长度

    public float zoomSpeed = 5f;//滚轮切换视野大小的速度
    public float minFOV = 20f;//最小视野宽
    public float maxFOV = 60f;//最大视野宽

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
        GUI.Label(labelRect, "箭:"+arrow.ToString(),style);
        labelRect = new Rect(10, 10, 200, 40);
        GUI.Label(labelRect, " 分  数 :"+Score.ToString(),style);
        if (areavalue > 1)
        {
            labelRect = new Rect(10, 40, 200, 80);
            GUI.Label(labelRect, "得分倍率:" + areavalue.ToString(), style);
        }

        if (shootArea) {
            // 设置准星颜色
            GUI.color = Color.black;

            // 绘制准星
            GUI.DrawTexture(new Rect(centerX - crosshairSize / 2f, centerY - 1f, crosshairSize, 2f), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(centerX - 1f, centerY - crosshairSize / 2f, 2f, crosshairSize), Texture2D.whiteTexture);

            // 恢复GUI颜色设置
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

    //MVC的视图部分
    void Update()
    {
        Camera cameraComponent = cam.GetComponent<Camera>();
        // 获取滚轮输入
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // 根据滚轮输入调整视野大小

        // 限制视野范围在最小值和最大值之间
        fieldOfView = Mathf.Clamp(fieldOfView - scrollInput * zoomSpeed, minFOV, maxFOV);
        // 应用新的视野大小

        cameraComponent.fieldOfView = fieldOfView;


    }

    //MVC的控制部分
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
        // 从预制中实例化游戏对象
        GameObject Arrow = Instantiate(Arrowprefab);
        Arrow.name = "Arrow";
        // 设置位置和朝向
        Arrow.transform.position = cam.transform.position;
        Arrow.transform.rotation = cam.transform.rotation;
        // 获取刚体组件
        Rigidbody rigidbody = Arrow.GetComponent<Rigidbody>();

        // 瞬移刚体到新位置
        Vector3 teleportPosition = cam.transform.position + cam.transform.rotation * Vector3.forward * 2;
        rigidbody.MovePosition(teleportPosition);

        // 计算初始速度向量
        Vector3 initialVelocity = cam.transform.forward * speed;

        // 设置初始速度
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