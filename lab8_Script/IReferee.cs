using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public interface IReferee
{
    public int getScore();
    public bool CheckGameOver();

    public void Restart();
    public void check();
    public void ShootDish(GameObject dish);
    public void SetController(ISceneController c);
}