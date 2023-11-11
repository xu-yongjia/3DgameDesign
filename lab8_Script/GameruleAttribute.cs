using System;
using UnityEngine;



[CreateAssetMenu(fileName = "RuleAttributes", menuName = "(ScriptableObject)RuleAttributes")]
public class GameruleAttribute : ScriptableObject
{
    [Tooltip("回合数")]
    public int trailNum;
    [Tooltip("每n回合增加一次飞碟数量")]
    public int increaseRound;
    [Tooltip("每次增加的飞碟数量")]
    public int increaseNum;
    [Tooltip("初始飞碟数量")]
    public int initNum;
    [Tooltip("3分飞碟比例")]
    public int Rate3;
    [Tooltip("2分飞碟比例")]
    public int Rate2;
    [Tooltip("1分飞碟比例")]
    public int Rate1;
    [Tooltip("3分飞碟速度")]
    public int Speed3;
    [Tooltip("2分飞碟速度")]
    public int Speed2;
    [Tooltip("1分飞碟速度")]
    public int Speed1;
}