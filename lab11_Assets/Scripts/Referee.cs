using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Referee : IReferee
{
    public FirstController controller;
    public static Referee Instance;
    public int Score{get; set;}
    public static Referee getInstance()//�ṩ�����ķ���
    {
        Debug.Log("getReferee");
        if (Instance == null)
        {
            Instance = new Referee();
        }
        return Instance;
    }

    // Update is called once per frame
    public void SetController(ISceneController c)
    {
        controller = c as FirstController;
    }
    public int getScore()
    {
        return Score;
    }
    public void Restart()
    {
        Score = 0;
    }
}
