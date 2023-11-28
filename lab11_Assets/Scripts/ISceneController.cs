using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISceneController
{//���ǽӿ�
    void LoadResources();
    void GameOver();
    void GameRestart();
    void startNextTurn();

    void Shoot(float speed);
    void PlayerCollisionEnter(Collider collision,GameObject msgsource,int msgtype,int value=0);
    void ArrowCollisionEnter(Collision collision,GameObject msgsource,int value);
}