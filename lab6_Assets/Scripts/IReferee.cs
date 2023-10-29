using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IReferee
{
    public void CheckGameOver();
    public void SetController(ISceneController c);
}