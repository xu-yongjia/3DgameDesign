using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Referee : MonoBehaviour,IReferee
{
    public FirstController controller;

    // Update is called once per frame
    public void CheckGameOver()
    {
        //ĳһ�����ʦ���ڶ�ħ
        if (controller.leftDevil > controller.leftPriest && controller.leftPriest != 0)
        {
            controller.GameOver(false);
        }
        if (controller.rightDevil > controller.rightPriest && controller.rightPriest != 0)
        {
            controller.GameOver(false);
        }
        if (controller.rightDevil + controller.rightPriest == 6)
        {
            controller.GameOver(true);
        }
    }
    public void SetController(ISceneController c)
    {
        controller = c as FirstController;
    }
}
