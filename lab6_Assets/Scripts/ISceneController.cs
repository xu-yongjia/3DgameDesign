using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISceneController
{//���ǽӿ�
    void LoadResources();
    void GameOver(bool win);
    void GameRestart();
    float getBoatPosition();
}