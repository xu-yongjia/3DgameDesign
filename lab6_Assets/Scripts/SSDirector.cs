using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SSDirector : System.Object
{
    private static SSDirector instance;
    public ISceneController currentSceneController { get; set; }//��¼���ṩ��ǰ���ǵķ���
    public static SSDirector getInstance()//�ṩ�����ķ���
    {
        if (instance == null)
        {
            instance = new SSDirector();
        }
        return instance;
    }
    public void setFPS(int fps)//�ṩfps����
    {
        Application.targetFrameRate = fps;
    }
}