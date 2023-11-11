using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Referee : IReferee
{
    public FirstController controller;
    public static Referee Instance;
    public int Score{get; set;}
    public static Referee getInstance()//提供单例的访问
    {
        Debug.Log("getReferee");
        if (Instance == null)
        {
            Instance = new Referee();
        }
        return Instance;
    }

    // Update is called once per frame
    public void check()
    {
        Debug.Log(controller.activeDish);
        bool b1 = CheckRoundOver();
        bool b2 = CheckGameOver();
        if (b1 && !b2)
        {
            controller.startNextTurn();
        }
    }
    public bool CheckRoundOver()
    {
        if (controller.activeDish == 0)
        {
            controller.curtrail++;
            return true;
        }
        return false;
    }
    public bool CheckGameOver()
    {
        if (controller.rule.trailNum == controller.curtrail)
        {
            controller.GameOver();
            return true;
        }
        return false;
    }
    public void ShootDish(GameObject dish)
    {
        switch (dish.name)
        {
            case "score1":
                Score += 1;
                break;
            case "score2":
                Score += 2;
                break;
            case "score3":
                Score += 3;
                break;
        }
        dish.GetComponent<Rigidbody>().position = new Vector3(-10000, 0, 0);
        controller.pool.ReturnObjectToPool(dish);
        controller.activeDish--;
    }
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
