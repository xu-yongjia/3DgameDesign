using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISceneController
{//���ǽӿ�
    void LoadResources();
    void GameOver();
    void GameRestart();
    void startNextTurn();

    void RemoveCollision(GameObject dish);
}