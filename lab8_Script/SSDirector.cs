using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SSDirector : System.Object
{
    private static SSDirector instance;
    public ISceneController currentSceneController { get; set; }//记录并提供当前场记的访问
    public static SSDirector getInstance()//提供单例的访问
    {
        if (instance == null)
        {
            instance = new SSDirector();
        }
        return instance;
    }
    public void setFPS(int fps)//提供fps设置
    {
        Application.targetFrameRate = fps;
    }
}