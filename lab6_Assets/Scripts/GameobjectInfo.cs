using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectInfo : MonoBehaviour
{
    //保存位置信息（区域+下标），进行空间上的移动（三维坐标）
    public int Area { set; get; }
    public int Index { set; get; }

    public bool clickable { set; get; } = true;
    public void UpdatePosition(int a, int b,bool movenow)
    {
        Area = a;
        Index = b;
        if (movenow)
        {
            Transform t = this.gameObject.transform;
            t.position = calPosition(a, b);
        }
    }
    Vector3 calPosition(int a, int b)//计算人物的位置
    {
        Vector3 res = new Vector3();
        Transform t = this.gameObject.transform;
        switch (a)
        {
            case 1://左岸
                res = new Vector3(0, 2, -7 - 2 * b);
                break;
            case 2://右岸
                res = new Vector3(0, 2, 7 + 2 * b);
                break;
            case 3://船
                res = new Vector3(0, 1.25f, SSDirector.getInstance().currentSceneController.getBoatPosition() + b * 1.2f - 0.6f);
                break;
        }
        return res;
    }
}