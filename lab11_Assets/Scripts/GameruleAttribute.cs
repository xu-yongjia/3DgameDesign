using System;
using UnityEngine;



[CreateAssetMenu(fileName = "RuleAttributes", menuName = "(ScriptableObject)RuleAttributes")]
public class GameruleAttribute : ScriptableObject
{
    [Tooltip("�غ���")]
    public int trailNum;
    [Tooltip("ÿn�غ�����һ�ηɵ�����")]
    public int increaseRound;
    [Tooltip("ÿ�����ӵķɵ�����")]
    public int increaseNum;
    [Tooltip("��ʼ�ɵ�����")]
    public int initNum;
    [Tooltip("3�ַɵ�����")]
    public int Rate3;
    [Tooltip("2�ַɵ�����")]
    public int Rate2;
    [Tooltip("1�ַɵ�����")]
    public int Rate1;
    [Tooltip("3�ַɵ��ٶ�")]
    public int Speed3;
    [Tooltip("2�ַɵ��ٶ�")]
    public int Speed2;
    [Tooltip("1�ַɵ��ٶ�")]
    public int Speed1;
}